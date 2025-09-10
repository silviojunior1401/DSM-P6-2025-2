import React from "react";
import { useNavigate } from "react-router-dom";
import { Activity, ArrowRight, Shield, Clock, Users } from "lucide-react";
import "./HomePage.css";

const HomePage: React.FC = () => {
    const navigate = useNavigate();

    return (
        <div className="home-container">
            <div className="welcome-section">
                <div className="welcome-card card">
                    <div className="welcome-icon">
                        <Activity size={48} />
                    </div>
                    <h2>Análise Rápida de Risco Cardíaco</h2>
                    <p>
                        Utilize nossa ferramenta baseada em IA para uma
                        avaliação precisa em minutos.
                    </p>

                    <div className="carousel-dots">
                        <span className="dot active"></span>
                        <span className="dot"></span>
                        <span className="dot"></span>
                    </div>

                    <button
                        onClick={() => navigate("/questionario")}
                        className="start-btn"
                    >
                        <span>Iniciar Avaliação</span>
                        <ArrowRight size={20} />
                    </button>
                </div>
            </div>

            <div className="features-section">
                <div className="feature-card card">
                    <Shield className="feature-icon" size={24} />
                    <h3>Seguro e Confiável</h3>
                    <p>Baseado em dados médicos validados</p>
                </div>

                <div className="feature-card card">
                    <Clock className="feature-icon" size={24} />
                    <h3>Resultado Rápido</h3>
                    <p>Análise completa em poucos minutos</p>
                </div>

                <div className="feature-card card">
                    <Users className="feature-icon" size={24} />
                    <h3>Para Profissionais</h3>
                    <p>Ferramenta desenvolvida para médicos</p>
                </div>
            </div>
        </div>
    );
};

export default HomePage;
