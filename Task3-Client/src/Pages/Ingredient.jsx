import React, { useState } from 'react'
import LoadingButton from '@mui/lab/LoadingButton';
import { TextField } from '@mui/material';
import { addIngredient } from "../Api";

export default function Ingredient() {
    const [name, setName] = useState('');
    const [image, setImage] = useState('');
    const [calories, setCalories] = useState('');
    const [loading, setLoading] = useState(false);

    const onNameChange = (event) => setName(event.target.value);
    const onImageChange = (event) => setImage(event.target.value);
    const onCaloriesChange = (event) => setCalories(event.target.value);

    const onSubmit = async (event) => {
        event.preventDefault();
        setLoading(true);
        try {
            await addIngredient({name,image,calories});
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
        setCalories("");
    }

    
  return (
    <div>
        <h1>Add New Ingredient</h1>
        <form className="form" onSubmit={onSubmit}>
        <TextField id="standard-basic" label="Name" variant="standard" fullWidth margin="normal" value={name}  onChange={onNameChange} required />
        <TextField id="standard-basic" label="Image URL" variant="standard" fullWidth  margin="normal" value={image} onChange={onImageChange} required />
        <TextField id="standard-basic" label="Calories" type="number" variant="standard" fullWidth margin="normal" value={calories} onChange={onCaloriesChange} required />
        <LoadingButton type="submit" variant="contained" loading={loading}>Create</LoadingButton>
        </form>
    </div>
  )
}