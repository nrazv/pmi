import React from "react";
import { Project } from "../../../models/Project";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Button,
  Card,
  Divider,
  styled,
  Typography,
} from "@mui/material";
import LanguageIcon from "@mui/icons-material/Language";
import AddIcon from "@mui/icons-material/Add";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

type Props = {
  project: Project;
};

const SubdomainsTab = ({ project }: Props) => {
  return (
    <StyledCard>
      <Box display="flex" justifyContent={"space-between"} marginBottom={4}>
        <Box sx={{ display: "flex", alignItems: "center" }}>
          <LanguageIcon color="primary" sx={{ fontSize: 25 }} />
          <Typography color="primary" variant="h6" ml={1}>
            Subdomains
          </Typography>
        </Box>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          sx={{ textTransform: "capitalize" }}
        >
          Add Subdomain
        </Button>
      </Box>
      <Box sx={{ maxHeight: "550px", overflowY: "scroll" }}>
        {project.subdomains.map((e, k) => (
          <StyledAccordion key={k}>
            <AccordionSummary
              sx={{ backgroundColor: "#0a0a0a" }}
              expandIcon={<ExpandMoreIcon />}
              aria-controls="panel1-content"
              id="panel1-header"
            >
              <SubdomainBox>
                <LanguageIcon sx={{ fontSize: 18 }} color="secondary" />
                <Typography
                  ml={1}
                  color="textDisabled"
                  variant="body1"
                  display={"inline-block"}
                >
                  {e.domain}
                </Typography>
              </SubdomainBox>
            </AccordionSummary>
            <AccordionDetails
              sx={{
                backgroundColor: "#0a0a0a",
              }}
            >
              <Box
                marginX={10}
                component="pre"
                sx={{
                  whiteSpace: "pre-wrap",
                  wordBreak: "break-word",
                  overflowWrap: "anywhere",
                  m: 0,
                  fontFamily: "inherit",
                }}
              >
                <Typography variant="subtitle1" color="primary">
                  {e.validationResult}
                </Typography>
              </Box>
            </AccordionDetails>
          </StyledAccordion>
        ))}
      </Box>
      <Divider sx={{ marginTop: 3 }} />
      <Typography
        color="textDisabled"
        variant="subtitle1"
        display={"inline-block"}
        mt={2}
      >
        Total: {project.subdomains.length} subdomains
      </Typography>
    </StyledCard>
  );
};

const StyledCard = styled(Card)(({ theme }) => ({
  padding: 25,
  flex: 1,
  borderRadius: 10,
  background: theme.palette.background.paper,
  border: `0.1rem solid ${theme.border.color.light}`,
  maxHeight: "840px",
}));

const SubdomainBox = styled(Box)(({ theme }) => ({
  marginTop: 10,
  padding: 10,
  display: "flex",
  alignItems: "center",
}));

const StyledAccordion = styled(Accordion)(({ theme }) => ({
  marginTop: 10,
  backgroundColor: "#0a0a0a",
  border: `1px solid ${theme.border.color.light}`,
  borderRadius: 5,

  "&::before": {
    display: "none",
  },

  "&.Mui-expanded": {
    borderRadius: 5,
  },

  "& .MuiAccordionSummary-root": {
    borderRadius: 5,
  },

  "& .MuiAccordionDetails-root": {
    borderBottomLeftRadius: 5,
    borderBottomRightRadius: 5,
  },
}));

export default SubdomainsTab;
