import React, { useState } from 'react';

const IzlenecekDizilerForm = ({ shows }) => {
    const [show, setShow] = useState(shows);
    return (
        <div>İzlenecek diziler içerikleri burada olacak - izlenecek diziler formu</div>
    )
}

export default IzlenecekDizilerForm;