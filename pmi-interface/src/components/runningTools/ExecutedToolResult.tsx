import useWebSocket from "react-use-websocket";
import React, { useEffect, useState } from "react";
import { ExecutedTool } from "../../models/ExecutedTool";

const URL = process.env.REACT_APP_WS_URL ?? "";

type Props = {
  toolOutput: string;
};

function ExecutedToolResult({ toolOutput }: Props) {
  return (
    <React.Fragment>
      <pre>{toolOutput}</pre>
    </React.Fragment>
  );
}

export default ExecutedToolResult;
