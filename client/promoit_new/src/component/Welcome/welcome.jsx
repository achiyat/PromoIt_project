import { useAuth0 } from "@auth0/auth0-react";
import React from "react";
import { Link, Route, Routes } from "react-router-dom";
import { About, ContactUs, Home } from "../../pages";
import "./welcome.css";

export const Welcome = (props) => {
  const { logout } = useAuth0();
  return (
    <>
      <header>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/User/about">About</Link>
            </li>
            <li>
              <Link to="/User/contact">Contact Us</Link>
            </li>
            <li>
              <button
                className="btn btn-danger"
                onClick={() => logout({ returnTo: window.location.origin })}
              >
                Log Out
              </button>
            </li>
          </ul>
        </nav>
      </header>
    </>
  );
};
