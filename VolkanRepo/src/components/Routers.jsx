import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import GirisEkrani from "./GirisEkrani";
import Anasayfa from "./Anasayfa"; 
const Routers = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<GirisEkrani />} />
        <Route path="/anasayfa" element={<Anasayfa/>} />
      </Routes>
    </Router>
  );
};

export default Routers;
