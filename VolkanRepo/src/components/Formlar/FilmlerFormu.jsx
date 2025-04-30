import React, { useState } from 'react';
const FilmlerFormu = ({films}) => {
    const [film, setFilm] = useState(films);

    return(
        <div> Film icerikleri burada olacak - filmlerformu</div>
    )

}

export default FilmlerFormu;