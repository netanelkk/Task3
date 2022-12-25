import React, { useEffect, useState } from 'react'
import { Alert } from '@mui/material';
import CircularProgress from '@mui/material/CircularProgress';
import { getRecipes } from "../Api";
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import Box from '@mui/material/Box';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';


const Ingredient = ({ingredient}) => {
  return (
    <Card sx={{ display: 'flex', margin: '20px 10px' }} key={ingredient["Name"]}>
    <Box sx={{ display: 'flex', flexDirection: 'column' }}>
      <CardContent sx={{ flex: '1 0 auto' }}>
        <Typography component="div" variant="h5">
          {ingredient["Name"]}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary" component="div">
        {ingredient["Calories"]} Calories
        </Typography>
      </CardContent>
    </Box>
    <CardMedia
      component="img"
      sx={{ height: 151 }}
      image={ingredient["Image"]}
      alt={ingredient["Name"]}
    />
  </Card>
  );
}

export default function Home() {
  const [recipesList, setRecipesList] = useState();
  const [totalCalories, setTotalCalories] = useState(0);
  useEffect(() => {
    const loadRecipes = async () => {
        try {
          let result = await getRecipes();
          setRecipesList(result);
          sumCalories(result);
        } catch (e) {
            alert("Error: "+e);
        }
    }

    loadRecipes();
}, []);

const sumCalories = (result) => {
  let total = 0;
  for(const recipe of result) {
    for(const ingredient of recipe["Ingredients"]) {
      total += ingredient["Calories"];
    }
  }

  setTotalCalories(total);
}
  return (
    <div>
        <h1>My Kitchen</h1>
        {recipesList == null ? <CircularProgress /> : "" }
        {(recipesList != null && recipesList.length == 0) ? <Alert severity="info">No Recipes Were Found</Alert> : ""}
        {(recipesList != null && recipesList.length > 0) ? <>
              {recipesList.map((recipe) => (
                    <Card sx={{ maxWidth: 345 }} key={recipe["Name"]}>
                    <CardMedia
                      sx={{ height: 240 }}
                      image={recipe["Image"]}
                      title={recipe["Name"]}
                    />
                    <CardContent>
                      <Typography gutterBottom variant="h5" component="div">
                      {recipe["Name"]}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        <b>Time:</b> {recipe["Time"]} Minutes <br />
                        <b>Cooking Method:</b> {recipe["CookingMethod"]} <br />
                        <b>Total Calories:</b> {totalCalories} <br /><br />
                        <u>Ingredients</u> <br />
                        {recipe["Ingredients"].map((ingredient) => (
                          <>
                          <Ingredient ingredient={ingredient} key={recipe["Name"]} />
                          </>
                        ))}
                      </Typography>
                    </CardContent>
                  </Card>
              ))}
            </>
        : ""}
        
    </div>
  )


}