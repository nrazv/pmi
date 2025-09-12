import React, { useEffect } from "react";

type Props = {
  lastMessage: MessageEvent<any> | null;
};

function RunningTool({ lastMessage }: Props) {
  const [response, setResponse] = React.useState<string>("");

  useEffect(() => {
    if (lastMessage !== null) {
      setResponse((prev) => prev.concat("\n" + lastMessage.data));
    }
    console.log(lastMessage);
  }, [lastMessage]);

  return <pre>{response}</pre>;
}

export default RunningTool;
