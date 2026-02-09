import "./App.css";
import { UserTable } from "./features/users/components/UserTable";
import { Toaster } from "react-hot-toast";

function App() {
  return (
    <>
      <Toaster position="top-right" reverseOrder={false} />
      <UserTable></UserTable>
    </>
  );
}

export default App;
