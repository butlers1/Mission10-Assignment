import { useEffect, useState } from "react";

interface Bowler {
  firstName: string;
  middleInit: string;
  lastName: string;
  teamName: string;
  address: string;
  city: string;
  state: string;
  zip: string;
  phone: string;
}

function BowlersTable() {
  const [bowlers, setBowlers] = useState<Bowler[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetch("https://localhost:5000/Bowlers")
      .then((res) => {
        if (!res.ok) throw new Error("Failed to fetch bowlers");
        return res.json();
      })
      .then((data) => {
        setBowlers(data);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  if (loading) return <p style={{ textAlign: "center" }}>Loading bowlers...</p>;
  if (error) return <p style={{ textAlign: "center", color: "red" }}>Error: {error}</p>;

  return (
    <div style={{ padding: "20px" }}>
      <table border={1} cellPadding={8} cellSpacing={0} style={{ width: "100%", borderCollapse: "collapse" }}>
        <thead style={{ backgroundColor: "#1a3a5c", color: "white" }}>
          <tr>
            <th>Bowler Name</th>
            <th>Team Name</th>
            <th>Address</th>
            <th>City</th>
            <th>State</th>
            <th>Zip</th>
            <th>Phone</th>
          </tr>
        </thead>
        <tbody>
          {bowlers.map((b, i) => (
            <tr key={i} style={{ backgroundColor: i % 2 === 0 ? "#f9f9f9" : "white" }}>
              <td>{`${b.firstName}${b.middleInit ? " " + b.middleInit + "." : ""} ${b.lastName}`}</td>
              <td>{b.teamName}</td>
              <td>{b.address}</td>
              <td>{b.city}</td>
              <td>{b.state}</td>
              <td>{b.zip}</td>
              <td>{b.phone}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default BowlersTable;