import React from "react";
import "./App.css";
import NavBar from "./components/NavBar";
import HomePage from "./pages/HomePage";

function App() {
  return (
    <React.Fragment>
      <NavBar />
      <HomePage />
    </React.Fragment>
  );
}

export default App;
