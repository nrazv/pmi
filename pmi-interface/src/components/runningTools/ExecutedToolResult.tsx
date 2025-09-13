import useWebSocket from "react-use-websocket";
import React, { useEffect, useState } from "react";
import { ExecutedTool } from "../../models/ExecutedTool";

const URL = process.env.REACT_APP_WS_URL ?? "";

type Props = {
  executedTool: ExecutedTool;
};

function ExecutedToolResult({ executedTool }: Props) {
  const { sendMessage, lastMessage } = useWebSocket(URL + "/toolResult");
  const [result, setResult] = useState(executedTool.executionResult);

  useEffect(() => {
    sendMessage(executedTool.id);

    if (lastMessage?.data) {
      setResult(lastMessage?.data);
    }
  }, [lastMessage]);

  return (
    <React.Fragment>
      <pre>{result}</pre>
    </React.Fragment>
  );
}

export default ExecutedToolResult;
