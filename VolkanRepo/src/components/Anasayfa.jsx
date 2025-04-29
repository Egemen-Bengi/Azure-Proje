import React, { useState } from "react";
import {
  DesktopOutlined,
  HistoryOutlined,
  AppstoreOutlined,
  HeartOutlined,
  RobotOutlined,
} from "@ant-design/icons";
import { Breadcrumb, Layout, Menu, theme } from "antd";
const { Header, Content, Footer, Sider } = Layout;
function getItem(label, key, icon, children) {
  return {
    key,
    icon,
    children,
    label,
  };
}
const items = [
  getItem("Filmler", "1", <AppstoreOutlined />),
  getItem("Diziler", "2", <DesktopOutlined />),
  getItem("Öneri Botu", "sub1", <RobotOutlined />, [
    getItem("Film Önerisi", "3"),
    getItem("Dizi Önerisi", "4"),
  ]),
  getItem("İzleme Listesi", "sub2", <HeartOutlined />, [
    getItem("İzlenecek Filmer", "6"),
    getItem("İzlenecek Diziler", "7"),
  ]),
  getItem("İzleme Geçmişi", "sub3", <HistoryOutlined />, [
    getItem("İzlediğim Filmler", "8"),
    getItem("İzlediğim Diziler", "9"),
  ]),
];

const Anasayfa = () => {
  const [collapsed, setCollapsed] = useState(false);
  const [selectedMenuLabel, setSelectedMenuLabel] = useState("Filmler"); // labelı saklamak için state
  const [selectedMenuKey, setSelectedMenuKey] = useState("1"); // seçili menü keyi için state
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleMenuClick = ({ key }) => {
    setSelectedMenuKey(key); // seçili menü keyini güncelle
    const selectedItem = items.find(
      (item) =>
        item.key === key || item.children?.some((child) => child.key === key)
    );
    if (selectedItem) {
      const label = selectedItem.children
        ? selectedItem.children.find((child) => child.key === key)?.label
        : selectedItem.label;
      setSelectedMenuLabel(label);
    }
  };

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <Sider
        collapsible
        collapsed={collapsed}
        onCollapse={(value) => setCollapsed(value)}
      >
        <div className="demo-logo-vertical" />
        <Menu
          theme="dark"
          defaultSelectedKeys={["1"]}
          mode="inline"
          items={items}
          onClick={handleMenuClick} // Menü tıklama olayını bağla
        />
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }}>
          <div
            style={{
              padding: "0 16px",
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
          >
            <h1 style={{ margin: 0 }}>{selectedMenuLabel}</h1>
          </div>
        </Header>
        <Content style={{ margin: "0 16px" }}>
          <Breadcrumb style={{ margin: "16px 0" }}>
            /* Burada breadcrumb öğelerini dinamik olarak oluşturabilirsin */
          </Breadcrumb>
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            {/* Seçili menüye göre içerik gösterimi  yapilacak
            her birisi icin form olusturarak kod gorunumu basitlestirilecek*/}
            {(() => {
              switch (selectedMenuKey) {
                case "1":
                  return <h2>Filmler İçeriği</h2>; 
                case "2":
                  return <h2>Diziler İçeriği</h2>; 
                case "3":
                  return <h2>Film Önerisi İçeriği</h2>; 
                case "4":
                  return <h2>Dizi Önerisi İçeriği</h2>; 
                case "6":
                  return <h2>İzlenecek Filmler İçeriği</h2>; 
                case "7":
                  return <h2>İzlenecek Diziler İçeriği</h2>; 
                case "8":
                  return <h2>İzlediğim Filmler İçeriği</h2>; 
                case "9":
                  return <h2>İzlediğim Diziler İçeriği</h2>; 
              }
            })()}
          </div>
        </Content>
        <Footer style={{ textAlign: "center" }}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
};
export default Anasayfa;
