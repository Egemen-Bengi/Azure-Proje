import React, { useState } from 'react';
import { Button, Input } from 'antd';

const DiziOneriFormu = () => {
    const [inputValue, setInputValue] = useState('');

    const handleInputChange = (event) => {
        setInputValue(event.target.value);
      }

    const handleSubmit = () => {
        //burada apiye veri gönderme işlemi yapılacak
        console.log(inputValue);
      };
  return (
    <div style={{ width: '100%', maxWidth: 600 }}>
      <h2>Dizi Öneri Botu</h2>
      <form
        style={{
          display: 'flex',
          flexDirection: 'column',
          gap: '16px',
          padding: '24px',
          backgroundColor: 'smokewhite',
          borderRadius: '8px',
          boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
        }}
      >
        <Input 
          placeholder="Izlediginiz bir dizinin adini giriniz"
          value={inputValue}
          onChange={handleInputChange}
        />
        <Button type="primary"  onClick={handleSubmit}> Gonder </Button>
      </form>
    </div>
  );
};

export default DiziOneriFormu;