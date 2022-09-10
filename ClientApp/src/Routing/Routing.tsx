import React from 'react';
import { Route, RouteObject, Routes , RoutesProps, useRoutes} from 'react-router-dom'
import { IRouting } from '../Models';

function Routing({routes}:IRouting) {
    // return(
    //     <Routes>
    //         {
    //           routes.filter(elem => !!elem.element).map((route,i) => {
    //             return <Route key={i} {...route}/>
    //           })
    //         }
    //     </Routes>      
    // );  
    return useRoutes(routes as Array<RouteObject>) 
}

export default Routing;