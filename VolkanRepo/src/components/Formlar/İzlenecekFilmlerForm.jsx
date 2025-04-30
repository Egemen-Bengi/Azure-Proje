import React, { useState } from "react";

const IzlenecekFilmlerForm = ({ shows }) => {
  const [show, setShow] = useState(shows);

  return (
    <div>İzlenecek film içerikleri burada olacak - izlenecek filmler formu</div>
  );
};
export default IzlenecekFilmlerForm;
