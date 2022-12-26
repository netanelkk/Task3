import React, { useEffect, useState } from 'react'
import { Alert } from '@mui/material';
import CircularProgress from '@mui/material/CircularProgress';
import { getRecipes } from "../Api";
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import Box from '@mui/material/Box';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';

const Ingredient = ({ingredient}) => {
  return (
    <Card sx={{ display: 'flex', margin: '20px 10px', alignItems: 'center' }} key={ingredient["Name"]}>
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
      sx={{ height: 100 }}
      image={ingredient["Image"]}
      alt={ingredient["Name"]}
    />
  </Card>
  );
}

export default function Home() {
  const [recipesList, setRecipesList] = useState();
  const [totalCalories, setTotalCalories] = useState([]);
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
  for(const recipe of result) {
    let total = 0;
    for(const ingredient of recipe["Ingredients"]) {
      total += ingredient["Calories"];
    }
    setTotalCalories(totalCalories => [...totalCalories, total]);
  }


}


  return (
    <div>
        <h1>My Kitchen</h1>
        {recipesList == null ? <CircularProgress /> : "" }
        {(recipesList != null && recipesList.length == 0) ? <Alert severity="info">No Recipes Were Found</Alert> : ""}
        {(recipesList != null && recipesList.length > 0) ? <div id="all-recipes"><Grid container spacing={2}>
              {recipesList.map((recipe,index) => (
                <Grid item xl={3} lg={4} sm={6} xs={12}>
                    <Card sx={{ display: 'inline-block', width: 345, margin: "10px 15px" }} key={recipe["Name"]}>
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
                        <b>Total Calories:</b> {totalCalories[index]} <br /><br />
                        <b>Ingredients:</b> <br />
                        {recipe["Ingredients"].map((ingredient) => (
                          <>
                          <Ingredient ingredient={ingredient} key={recipe["Name"]} />
                          </>
                        ))}
                      </Typography>
                    </CardContent>
                    </Card>
                  </Grid>
              ))}
              </Grid></div>
        : ""}
        
    </div>
  )


}