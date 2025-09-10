import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api";
import { type IAvaliacao } from "../types";

const HistoricoPage: React.FC = () => {
    const [historico, setHistorico] = useState<IAvaliacao[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchHistorico = async () => {
            try {
                const response = await api.get<IAvaliacao[]>("/historico");
                // Ordena do mais recente para o mais antigo
                const sortedData = response.data.sort(
                    (a, b) =>
                        new Date(b.data).getTime() - new Date(a.data).getTime()
                );
                setHistorico(sortedData);
            } catch {
                setError("Não foi possível carregar o histórico.");
            } finally {
                setLoading(false);
            }
        };

        fetchHistorico();
    }, []);

    const handleViewResult = (avaliacao: IAvaliacao) => {
        const resultado = {
            predicao: avaliacao.resultado,
            recomendacao:
                avaliacao.resultado === 1
                    ? "Paciente apresenta alto risco..." // Coloque a recomendação completa aqui
                    : "Paciente apresenta baixo risco...",
        };
        navigate("/resultado", {
            state: { questionario: avaliacao.questionario, resultado },
        });
    };

    if (loading) return <p>Carregando histórico...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div>
            <h2>Histórico de Avaliações</h2>
            {historico.length === 0 ? (
                <p>Nenhuma avaliação encontrada.</p>
            ) : (
                historico.map((item) => (
                    <div
                        key={item.data}
                        onClick={() => handleViewResult(item)}
                        style={{
                            cursor: "pointer",
                            border: "1px solid #ccc",
                            margin: "10px",
                            padding: "10px",
                        }}
                    >
                        <p>
                            <strong>Paciente:</strong> {item.questionario.nome}
                        </p>
                        <p>
                            <strong>Data:</strong>{" "}
                            {new Date(item.data).toLocaleString("pt-BR")}
                        </p>
                        <p
                            style={{
                                color: item.resultado === 1 ? "red" : "green",
                            }}
                        >
                            <strong>Resultado:</strong>{" "}
                            {item.resultado === 1
                                ? "ALTO RISCO"
                                : "BAIXO RISCO"}
                        </p>
                    </div>
                ))
            )}
        </div>
    );
};

export default HistoricoPage;
