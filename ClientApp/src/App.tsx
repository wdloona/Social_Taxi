import React, { useContext, useEffect } from 'react';
import { NotificationContext, NotificationService } from './Services';
import { Layout } from 'antd';
import { HeaderContent } from './Components/Layout';
import LeftSideControls from './Components/LeftSideControlls';
import Routing from './Routing/Routing';
import Routes from './Routing/Routes';
import { Map } from './Pages/Home/Components';

const { Header, Content, Sider } = Layout;

const App = () => {

  const notifyContext = useContext(NotificationContext);

  useEffect(() => {
    const Init = async () => {
      let ns = new NotificationService(notifyContext);
      await ns.Init();
    }
    Init();
  }, []);



  return (

    <div className='h-screen relative'>
      <Map className="absolute z-0"/>
      <LeftSideControls className="relative inline-flex top-0 left-0 z-[999] m-8 max-w-[600]"/>
    </div>



    // <Layout className="min-h-screen">
    //   <Header className="header flex px-0 pr-6 bg-zinc-100 items-center">
    //     <HeaderContent/>
    //   </Header>
    //   <Layout>
    //     <Sider
    //       width={200}
    //       className="site-layout-background w-80 flex-none max-w-none bg-zinc-100 px-3 pt-10 pb-6"
    //     >
    //       <div className="w-full h-full">
    //         <LeftSideControls/>
    //       </div>
    //     </Sider>
    //     <Layout style={{ padding: "0 24px 24px" }} className="bg-zinc-100 pt-10">
    //       <Content
    //         className="site-layout-background bg-white rounded-lg"
    //         style={{
    //           margin: 0,
    //           minHeight: 280,
    //         }}
    //       >
    //         <Routing routes={Routes} />
    //       </Content>
    //     </Layout>
    //   </Layout>
    // </Layout>
  );
}

export default App;
{/* <Routing routes={Routes} /> */ }