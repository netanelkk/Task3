import './App.css';
import Button from '@mui/material/Button';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import AddTaskIcon from '@mui/icons-material/AddTask';
import PlaylistAddIcon from '@mui/icons-material/PlaylistAdd';
import { Routes, Route, Link } from 'react-router-dom';

import Home from './Pages/Home';
import Ingredient from './Pages/Ingredient';
import Recipe from './Pages/Recipe';

function App() {

  
  return (
    <div className="App">
      <header className="App-header">
        <Link to="/"><Button variant="text" startIcon={<MenuBookIcon />}>My Kitchen</Button></Link>
        <Link to="/ingredient"><Button variant="text" startIcon={<AddTaskIcon />}>Create New Ingredient </Button></Link>
        <Link to="/recipe"><Button variant="text" startIcon={<PlaylistAddIcon />}>Create New Recipe</Button></Link>
      </header>
      <div className='main-page'>
      <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/ingredient" element={<Ingredient />} />
          <Route path="/recipe" element={<Recipe />} />
      </Routes>
      </div>
    </div>
  );
}

export default App;
