import * as React from "react";
import { createRoot } from "react-dom/client";

import "@mantine/core/styles.css";
import "@mantine/dates/styles.css";

import "./index.css";
import { App } from "./app";

const root = document.getElementById("root");
if (!root) throw new Error("No root element found");

createRoot(root).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
