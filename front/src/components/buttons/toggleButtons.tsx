import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";

interface ToggleButtonsProps {
  textButton1: string;
  textButton2: string;
  variantButton1?: string;
  variantButton2?: string;
  onClickButton1: () => void;
  onClickButton2: () => void;
  layout?: string;
  setShadowDefault?: boolean;
}

const ToggleButtons: React.FC<ToggleButtonsProps> = ({
  textButton1,
  textButton2,
  variantButton1,
  variantButton2,
  onClickButton1,
  onClickButton2,
  setShadowDefault = true,
}) => {
  const [colorSelected, setColorSelected] = useState<string>("");

  useEffect(() => {
    if (setShadowDefault) {
      setColorSelected("primary");
    }
  }, []);

  function activeBtnPrimary() {
    onClickButton1();
    setColorSelected("primary");
  }
  function activeBtnSecondary() {
    onClickButton2();
    setColorSelected("secondary");
  }
  return (
    <div
      className="m-3 m-sm-4 w-100 d-flex gap-1 gap-sm-2 container "
      style={{ maxWidth: "1000px" }}
    >
      <Button
        variant={variantButton1 != undefined ? variantButton1 : "primary"}
        onClick={activeBtnPrimary}
        className={`w-100 ${
          colorSelected === "primary" ? "active" : ""
        } p-md-3`}
      >
        {textButton1}
      </Button>
      <Button
        variant={variantButton2 != undefined ? variantButton2 : "secondary"}
        onClick={activeBtnSecondary}
        className={`w-100 ${
          colorSelected === "secondary" ? "active" : ""
        } p-md-3`}
      >
        {textButton2}
      </Button>
    </div>
  );
};

export default ToggleButtons;
