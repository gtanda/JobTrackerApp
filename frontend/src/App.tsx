import Login from "./Components/Login/Login.tsx";
import {useContext} from "react";
import {AuthContext} from "./Components/Auth/AuthContext.tsx";
import Dashboard from "./Components/Dashboard/Dashboard.tsx";

function App() {
  const {accessToken} = useContext(AuthContext);
  return (<>
    {accessToken ? <Dashboard/> : <Login/> }
  </>)
}

export default App
