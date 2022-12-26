
CREATE TABLE tar3_Ingredient (
	[id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[image] [varchar](150) NOT NULL,
	[calories] [int] NOT NULL
)

CREATE TABLE tar3_Recipe (
	[id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[image] [varchar](150) NOT NULL,
	[cookingMethod] [varchar](255) NOT NULL,
	[time] [int] NOT NULL
)

CREATE TABLE tar3_IngredientsInRecipes (
	[recipeId] [int] REFERENCES tar3_Recipe(id),
	[ingredientId] [int] REFERENCES tar3_Ingredient(id),
	primary key(recipeId,ingredientId)
)