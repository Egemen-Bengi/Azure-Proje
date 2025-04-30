import React, { useState } from "react";
import {
  DesktopOutlined,
  HistoryOutlined,
  AppstoreOutlined,
  HeartOutlined,
  RobotOutlined,
} from "@ant-design/icons";
import { Breadcrumb, Layout, Menu, theme } from "antd";
import "./cssDosyalari/Anasayfa.css"; // CSS dosyasını içe aktarın
import FilmOneriFormu from "./Formlar/FilmOneriForm";
import DiziOneriFormu from "./Formlar/DiziOneriFormu";
import FilmlerFormu from "./Formlar/FilmlerFormu";
import DizilerFormu from "./Formlar/DizilerFormu";
import IzlenecekFilmlerForm from "./Formlar/İzlenecekFilmlerForm";
import IzlenecekDizilerForm from "./Formlar/İzlenecekDizilerForm";

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
  const [selectedMenuLabel, setSelectedMenuLabel] = useState("Filmler");
  const [selectedMenuKey, setSelectedMenuKey] = useState("1");
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleMenuClick = ({ key }) => {
    setSelectedMenuKey(key);
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
    <Layout className="layout">
      <Sider
        className="sider"
        collapsible
        collapsed={collapsed}
        onCollapse={(value) => setCollapsed(value)}
      >
        <div className="demo-logo-vertical" />
        <div className="sider-header">
          
        </div>
        <Menu
          theme="dark"
          defaultSelectedKeys={["1"]}
          mode="inline"
          items={items}
          onClick={handleMenuClick}
        />
      </Sider>
      <Layout>
        <Header className="header">
          <div className="header-content">
            <h1 className="header-title">{selectedMenuLabel}</h1>
          </div>
        </Header>
        <Content className="content">
          <Breadcrumb className="breadcrumb">
            {/* Burada breadcrumb öğelerini dinamik olarak oluşturabilirsiniz */}
          </Breadcrumb>
          <div className="content-container">
            {(() => {
              switch (selectedMenuKey) {
                case "1":
                  return <h2><FilmlerFormu></FilmlerFormu></h2>;
                case "2":
                  return <h2><DizilerFormu></DizilerFormu></h2>;
                case "3":
                  return <h2><FilmOneriFormu></FilmOneriFormu></h2>;
                case "4":
                  return <h2><DiziOneriFormu></DiziOneriFormu></h2>;
                case "6":
                  return <h2><IzlenecekFilmlerForm/></h2>;
                case "7":
                  return <h2><IzlenecekDizilerForm/></h2>;
                case "8":
                  return <h2>İzlediğim Filmler İçeriği</h2>;
                case "9":
                  return <h2>İzlediğim Diziler İçeriği</h2>;
                default:
                  return null;
              }
            })()}
          </div>
        </Content>
        <Footer className="footer">
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
};

export default Anasayfa;
