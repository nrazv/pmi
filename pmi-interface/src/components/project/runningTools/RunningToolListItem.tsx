import useWebSocket from "react-use-websocket";
import { ToolExecuteRequest } from "../../../models/ToolExecuteRequest";

const URL = process.env.REACT_APP_WS_URL ?? "";

type Props = {
  request: ToolExecuteRequest | null;
};

function RunningToolListItem({ request }: Props) {
  const { sendJsonMessage, lastJsonMessage } = useWebSocket(URL);

  const runTool = () => {
    sendJsonMessage(request);
  };

  return <div>RunningToolListItem</div>;
}

export default RunningToolListItem;
