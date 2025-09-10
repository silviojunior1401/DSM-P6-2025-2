import React from "react";
import { NavLink } from "react-router-dom";
import { Home, Heart, FileText } from "lucide-react";
import "./NavigationBar.css";

const NavigationBar: React.FC = () => {
    return (
        <nav className="navigation-bar">
            <NavLink
                to="/home"
                className={({ isActive }) =>
                    `nav-item ${isActive ? "nav-item--active" : ""}`
                }
            >
                <Home size={20} />
                <span>Início</span>
            </NavLink>

            <NavLink
                to="/questionario"
                className={({ isActive }) =>
                    `nav-item ${isActive ? "nav-item--active" : ""}`
                }
            >
                <Heart size={20} />
                <span>HealthCheck</span>
            </NavLink>

            <NavLink
                to="/historico"
                className={({ isActive }) =>
                    `nav-item ${isActive ? "nav-item--active" : ""}`
                }
            >
                <FileText size={20} />
                <span>Histórico</span>
            </NavLink>
        </nav>
    );
};

export default NavigationBar;
