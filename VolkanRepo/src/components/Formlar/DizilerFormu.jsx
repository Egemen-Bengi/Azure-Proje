import React, { useEffect, useState } from 'react';
import { Button, Card, Flex, Typography } from 'antd';;
const DizilerFormu = () => {
    const [tvSeries, setTvSeries] = useState(
        [
            {
              "id": 2,
              "diziAdi": "Breaking Bad",
              "diziAciklamasi": "Ameliyat edilemez akciger kanseri teshisi konulan kimya ögretmeni, ailesinin gelecegini güvence altina almak için eski bir ögrencisiyle birlikte metamfetamin üretip satmaya baslar.",
              "tarih": "2008-01-01",
              "sure": 45,
              "sezonSayisi": 5
            },
            {
              "id": 3,
              "diziAdi": "Band of Brothers",
              "diziAciklamasi": "ABD Ordusu 101. Hava Indirme Tümeni'ne bagli Easy Bölügü'nün II. Dünya Savasi Avrupa'sindaki görevi, Overlord Harekati'ndan VJ Günü'ne kadar uzanan hikayesi.",
              "tarih": "2001-01-01",
              "sure": 60,
              "sezonSayisi": 1
            },
            {
              "id": 4,
              "diziAdi": "Chernobyl",
              "diziAciklamasi": "Nisan 1986'da Sovyetler Birligi'ndeki Çernobil sehri insanlik tarihindeki en kötü nükleer felaketlerden birini yasar. Sonuç olarak, birçok kahraman sonraki günlerde, haftalarda ve aylarda hayatlarini tehlikeye atar.",
              "tarih": "2019-01-01",
              "sure": 60,
              "sezonSayisi": 1
            },
            {
              "id": 5,
              "diziAdi": "The Wire",
              "diziAciklamasi": "Baltimore'daki uyusturucu sahnesi, uyusturucu saticilarinin ve kolluk kuvvetlerinin gözünden.",
              "tarih": "2002-01-01",
              "sure": 60,
              "sezonSayisi": 5
            },
            {
              "id": 6,
              "diziAdi": "Avatar The Last Airbender",
              "diziAciklamasi": "Savaslarla harap olmus, temel güçlerin hüküm sürdügü bir dünyada, genç bir çocuk Avatar olarak kaderini yerine getirmek ve dünyaya baris getirmek için tehlikeli bir mistik göreve çikmak üzere yeniden uyanir.",
              "tarih": "2005-01-01",
              "sure": 23,
              "sezonSayisi": 3
            },
            {
              "id": 7,
              "diziAdi": "Game of Thrones",
              "diziAciklamasi": "Dokuz asil aile, Westeros topraklari üzerinde kontrol sahibi olmak için savasirken, kadim bir düsman binlerce yildir uykuda kaldiktan sonra geri dönüyor.",
              "tarih": "2011-01-01",
              "sure": 60,
              "sezonSayisi": 8
            },
            {
              "id": 8,
              "diziAdi": "Rick and Morty",
              "diziAciklamasi": "Nihilist bir çilgin bilim adaminin ve endiseli torununun parçalanmis aile hayatlari, boyutlar arasi maceralariyla daha da karmasik bir hal alir.",
              "tarih": "2013-01-01",
              "sure": 21,
              "sezonSayisi": 12
            },
            {
              "id": 9,
              "diziAdi": "Arcane",
              "diziAciklamasi": "Ikiz sehirler Piltover ve Zaun'un sert anlasmazliklari ortasinda, iki kiz kardes büyü teknolojileri ve çatisan inançlar arasindaki bir savasin rakip taraflarinda mücadele eder.",
              "tarih": "2021-01-01",
              "sure": 40,
              "sezonSayisi": 2
            }
        ]
    );
    const [likedSeries, setLikedSeries] = useState([]);
    const [imageName, setImageName] = useState("arcane.jpg");
    const imagePath = `/tvSeries/`;

    const cardStyle = {
        width: 700,
    };
    const imgStyle = {
        display: 'block',
        width: 200,
    };

    const handleOnClick = (serie) => {
        console.log(serie)
    };

    return (
        <div>
            {
                tvSeries.map((series) => {
    
                    const formattedName = series.diziAdi.toLowerCase().replace(/\s+/g, '');

                    return (
                        <Card hoverable style={cardStyle} bodyStyle={{ padding: 0, overflow: 'hidden' }} key={series.id}>
                            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <img
                                    alt="avatar"
                                    src={`${imagePath}${formattedName}.jpg`} // Dinamik resim yolu
                                    style={imgStyle}
                                />
                                <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', justifyContent: 'space-between', padding: 32 }}>
                                    <Typography.Title level={3}>
                                        {series.diziAciklamasi}
                                    </Typography.Title>
                                    <Button type="primary" onClick={() => handleOnClick(series)}>
                                        Beğen
                                    </Button>
                                </div>
                            </div>
                        </Card>
                    );
                })
            }
        </div>
    );
};

export default DizilerFormu;