import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { useEffect, useRef } from "react";

export const useToolHub = (
  executionId: string,
  onReceiveOutput: (data: string) => void
) => {
  const connectionRef = useRef<HubConnection | null>(null);

  useEffect(() => {
    const startConnection = async () => {
      const connection = new HubConnectionBuilder()
        .withUrl("http://localhost:8080/toolHub")
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

      connection.on("ReceiveOutput", (data: string) => {
        console.log("Received output:", data);
        onReceiveOutput(data);
      });

      try {
        await connection.start();
        await connection.invoke("JoinToolGroup", executionId);
        console.log(`Joined ToolGroup: ${executionId}`);
      } catch (err) {
        console.error("Connection error:", err);
      }

      connectionRef.current = connection;
    };

    startConnection();

    return () => {
      const cleanup = async () => {
        try {
          if (connectionRef.current?.state === "Connected") {
            await connectionRef.current.invoke("LeaveToolGroup", executionId);
          }
          await connectionRef.current?.stop();
          console.log(`Disconnected from ToolGroup: ${executionId}`);
        } catch (err) {
          console.error("Disconnect error:", err);
        }
      };

      cleanup();
    };
  }, [executionId, onReceiveOutput]);

  return connectionRef;
};
