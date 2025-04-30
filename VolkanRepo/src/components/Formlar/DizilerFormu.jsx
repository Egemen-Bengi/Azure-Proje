import React, { useState } from 'react';
const DizilerFormu = ({shows}) => {
    const [show, setShow] = useState(shows);

    return(
        <div> Dizi icerikleri burada olacak - dizilerformu</div>
    )

}

export default DizilerFormu;