import First from '../Pages/First/First';
import Main from '../Pages/Main/Main';
import Second from '../Pages/Second/Second';
import Login from '../Pages/Login/Login';
import { IRoutingProp } from '../Models';
import Home from '../Pages/Home';


const Routes:Array<IRoutingProp> = [
    { path:'/first', element:<First/>},
    { path:'/map',element:<Home/> },
    { path: '/second', element: <Second /> },
    { path: "/login", element: <Login/> },
    { path:'/', element: <Main/> }
];

export default Routes;