import React from "react";
import useWebSocket from "react-use-websocket";

const URL = "ws://localhost:8080/ws";

function RunningToolsPanel() {
  const { sendJsonMessage, lastMessage } = useWebSocket(URL);
  return <div>RunningToolsPanel</div>;
}

export default RunningToolsPanel;
