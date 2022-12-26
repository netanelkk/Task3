import React, { useEffect, useState } from 'react'
import LoadingButton from '@mui/lab/LoadingButton';
import { TextField } from '@mui/material';
import { addRecipe, getIngredients } from "../Api";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import CircularProgress from '@mui/material/CircularProgress';

export default function Recipe() {
    const [name, setName] = useState('');
    const [cookingMethod, setCookingMethod] = useState('');
    const [time, setTime] = useState('');
    const [image, setImage] = useState('');
    const [loading, setLoading] = useState(true);
    const [ingredientsIds, setIngredientsIds] = useState([]);
    const [ingredientsList, setIngredientsList] = useState([]);


    const onNameChange = (event) => setName(event.target.value);
    const onCookingMethodChange = (event) => setCookingMethod(event.target.value);
    const onTimeChange = (event) => setTime(event.target.value);
    const onImageChange = (event) => setImage(event.target.value);
    const ingredientChange = (event) => {
        setIngredientsIds(typeof event.target.value === 'string' ? event.target.value.split(',') : event.target.value);
    };

    useEffect(() => {
        const loadIngredients = async () => {
            try {
                const ings = await getIngredients();
                if(ings.length > 0) {
                    setIngredientsList(ings);
                    setLoading(false);
                }else{
                    alert("Add Some Ingredients First");
                }

            } catch (e) {
                alert("Error: "+e);
            }
        }

        loadIngredients();
    }, []);

    const onSubmit = async (event) => {
        event.preventDefault();
        setLoading(true);
        try {
            await addRecipe({"Name": name,
                             "CookingMethod": cookingMethod,
                             "Time": time,
                             "Image": image,
                             "Ingredients_ids": ingredientsIds});
            clearForm();
            alert("Added Successfully!");
        } catch(e) {
            alert("Error: "+e);
        }
        setLoading(false);
    };

    function clearForm() {
        setName("");
        setImage("");
        setCookingMethod("");
        setTime("");
        setIngredientsIds([]);
    }


  return (
    <div>
        <h1>Add New Recipe</h1>
        <form className="form" onSubmit={onSubmit}>
        <TextField id="standard-basic" label="Name" variant="standard" fullWidth margin="normal" value={name}  onChange={onNameChange} required />
        <TextField id="standard-basic" label="Cooking Method" variant="standard" fullWidth  margin="normal" value={cookingMethod} onChange={onCookingMethodChange} required />
        <TextField id="standard-basic" label="Time" type="number" variant="standard" fullWidth margin="normal" value={time} onChange={onTimeChange} required />
        <TextField id="standard-basic" label="Image URL" variant="standard" fullWidth  margin="normal" value={image} onChange={onImageChange} required />
        
        {(ingredientsList.length > 0) ? <FormControl margin="normal" fullWidth>
            <InputLabel id="select-ing-label">Select Ingredients..</InputLabel>
            <Select
                labelId="select-ing-label"
                id="select-ing"
                value={ingredientsIds}
                label="Select Ingredients.."
                onChange={ingredientChange}
                multiple
            >
            
            {ingredientsList.map((ing) => (
            <MenuItem value={ing["Id"]} key={ing["Id"]}>
                <img src={ing["Image"]} id="ingredient-icon" /> {ing["Name"]}
            </MenuItem>
            ))}
            </Select>
        </FormControl> : <div id="ingredients-loading"><CircularProgress /></div> }
        <LoadingButton type="submit" variant="contained" loading={loading}>Create</LoadingButton>
        </form>
    </div>
  )
}