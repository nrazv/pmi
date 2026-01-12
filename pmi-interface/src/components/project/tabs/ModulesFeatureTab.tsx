import React, { useEffect, useState } from "react";
import { Project } from "../../../models/Project";
import {
  Box,
  Button,
  Card,
  FormControl,
  styled,
  Typography,
} from "@mui/material";
import { Zap } from "lucide-react";
import { ModuleSelect } from "../../ModuleSelect";
import { executeModule, fetchModules } from "../../../services/ApiService";
import { Module } from "../../../models/Modules";
import SelectTarget from "../../SelectTarget";
import { ModuleExecutionRequest } from "../../../models/ModuleExecutionRequest";

type Props = {
  project: Project;
};

const ModulesFeatureTab = ({ project }: Props) => {
  const [modules, setModules] = useState<Module[]>([]);
  const [executionRequest, setRequest] = useState<ModuleExecutionRequest>({
    projectName: project.name,
    target: "",
    moduleName: "",
  });

  const handleTargetChange = (target: string): void => {
    setRequest({ ...executionRequest, target: target });
  };

  const handleModuleChange = (target: string): void => {
    setRequest({ ...executionRequest, moduleName: target });
  };

  const clearForm = (): void => {
    setRequest({
      projectName: project.name,
      target: "",
      moduleName: "",
    });
  };
  const isDisabled =
    !executionRequest.moduleName.trim() || !executionRequest.target.trim();

  const execute = async () => {
    const response = await executeModule(executionRequest);
    clearForm();
  };

  useEffect(() => {
    const loadModules = async () => {
      const modules = await fetchModules();
      setModules(modules);
    };
    loadModules();
  }, []);

  return (
    <div>
      <StyledCard>
        {Header}
        <Typography variant="body1" color="textDisabled">
          Select Module
        </Typography>
        <FormControl fullWidth sx={{ marginTop: 2 }}>
          <ModuleSelect modules={modules} moduleChange={handleModuleChange} />
          <SelectTarget
            selectTarget={handleTargetChange}
            selectedTarget={executionRequest.target}
            project={project}
          />
        </FormControl>
        <Button
          fullWidth
          variant="contained"
          sx={{ marginTop: 3 }}
          onClick={execute}
          disabled={isDisabled}
        >
          Run Tool
        </Button>
      </StyledCard>
    </div>
  );
};

const Header = (
  <Box display="flex" marginY={2}>
    <Zap color="#39FF14" />
    <Typography color="primary" ml={1}>
      Modules
    </Typography>
  </Box>
);

const StyledCard = styled(Card)(({ theme }) => ({
  padding: 20,
  flex: 1,
  borderRadius: 10,
  background: theme.palette.background.paper,
  border: `0.1rem solid ${theme.border.color.light}`,
}));
export default ModulesFeatureTab;
