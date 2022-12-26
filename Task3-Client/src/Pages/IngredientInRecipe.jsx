import React from 'react'
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import Typography from '@mui/material/Typography';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';

function IngredientInRecipe ({ingredient}) {
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

  export { IngredientInRecipe }