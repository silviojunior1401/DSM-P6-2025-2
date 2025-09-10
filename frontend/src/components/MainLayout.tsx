import React from "react";
import { useNavigate } from "react-router-dom";
import { LogOut } from "lucide-react";
import NavigationBar from "./NavigationBar";
import "./MainLayout.css";

interface MainLayoutProps {
    children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("email");
        navigate("/", { replace: true });
    };

    return (
        <div className="main-layout">
            <header className="main-header">
                <div className="header-content">
                    <h1 className="app-title">
                        HealthCheck <span className="heart-icon">❤️</span>
                    </h1>
                    <button onClick={handleLogout} className="logout-btn">
                        <LogOut size={18} />
                        Sair
                    </button>
                </div>
            </header>

            <main className="main-content">{children}</main>

            <NavigationBar />
        </div>
    );
};

export default MainLayout;
