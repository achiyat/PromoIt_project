import { useAuth0 } from "@auth0/auth0-react";
import "./App.css";
import { Login, Welcome } from "./component";

function App() {
  const { isAuthenticated, isLoading } = useAuth0();

  if (isLoading) {
    return (
      <div>
        <h1>loading init auth</h1>
      </div>
    );
  } else {
    return isAuthenticated ? <Welcome /> : <Login />;
  }
}

export default App;
