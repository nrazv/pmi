import { Box, Paper } from "@mui/material";
import useWebSocket from "react-use-websocket";

const URL = "ws://localhost:8080/ws";

function RunningToolsPanel() {
  const { sendJsonMessage, lastMessage } = useWebSocket(URL);
  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "space-evenly",
      }}
    >
      <Paper sx={{ width: "50%", margin: 1, height: "75vh" }}>
        <h1>Box1</h1>
      </Paper>
      <Paper sx={{ width: "50%", margin: 1 }}>
        <h1>Box2</h1>
      </Paper>
    </Box>
  );
}

export default RunningToolsPanel;
