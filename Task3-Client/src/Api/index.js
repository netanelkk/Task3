
let API_URL = "http://localhost:61034/api";

async function request(url, data, requestMethod="POST") {
    let request = { 
        method: requestMethod,
        headers: { "Content-Type": "application/json" }
    };
  
    if(requestMethod == "POST") {
      request.body = JSON.stringify(data);
    }
  

    return await fetch(url, request).then((response) => {
        if(response.status != 200) {
            throw response.statusText; 
        }
        return response.json();
      }).then((result) => {
        return result;
      });

}

export async function getIngredients() {
    return await request(API_URL+"/ingredients", "", "GET");
}

export async function getRecipes() {
    return await request(API_URL+"/recipes", "", "GET");
}

export async function addIngredient({name, image, calories}) {
    return await request(API_URL+"/ingredients", {name, image, calories});
}

export async function addRecipe(recipe) {
    return await request(API_URL+"/recipes", recipe);
}