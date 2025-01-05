import { IconButton, Toolbar, Typography } from "@mui/material";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import { Project } from "../../shared/Project";

type Props = {
  project?: Project;
};

function ProjectToolBar({ project }: Props) {
  return (
    <Toolbar variant="dense">
      <Typography variant="h6" component="div" color="black">
        {project?.name}
      </Typography>

      <IconButton
        edge="start"
        sx={{ marginRight: 0, marginLeft: "auto", padding: 0 }}
      >
        <DeleteForeverIcon color="error" fontSize="large" />
      </IconButton>
    </Toolbar>
  );
}

export default ProjectToolBar;
