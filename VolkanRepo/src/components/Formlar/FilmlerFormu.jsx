import React, { useState, useEffect } from "react";

const FilmlerFormu = () => {
  const [filmler, setFilmler] = useState([]);
  const [yukleniyor, setYukleniyor] = useState(true);
  const [hata, setHata] = useState(null);

  useEffect(() => {
    fetch(`'http://localhost:5000'/api/diziler`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Veri alınamadı");
        }
        return response.json();
      })
      .then((data) => {
        if (!data || !Array.isArray(data)) {
          throw new Error("Beklenmeyen veri formatı");
        }
        setFilmler(data);
        setYukleniyor(false);
      })
      .catch((err) => {
        setHata(err.message);
        setYukleniyor(false);
      });
  }, []);

  if (yukleniyor) return <div className="loading">Yükleniyor...</div>;
  if (hata) return <div style={{ color: "red" }}>Hata: {hata}</div>;
  if (!yukleniyor && filmler.length === 0) {
    return <div>Gösterilecek film bulunamadı.</div>;
  }

  return (
    <div>
      <h2>Filmler</h2>
      <ul className="film-list">
        {filmler.map((film) => (
          <li key={film.filmId} className="film-item">
            <h3>{film.filmAdi}</h3>
            <p>
              <strong>Yayın Yılı:</strong> {new Date(film.tarih).getFullYear()}
            </p>{" "}
            <p>
              <strong>Açıklama:</strong> {film.filmAciklamasi}
            </p>
            <p>
              <strong>Bölüm Süresi:</strong> {film.sure} Dakika
            </p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default FilmlerFormu;
