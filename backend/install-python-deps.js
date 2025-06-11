const { exec } = require("child_process");

function installWithPythonCmd(cmd) {
    exec(`${cmd} -m pip install --upgrade pip`, (err, stdout, stderr) => {
        if (err) {
            if (cmd === "python") {
                // Tenta com python3
                installWithPythonCmd("python3");
            } else {
                console.error("Erro ao atualizar o pip:", stderr);
                process.exit(1);
            }
        } else {
            exec(
                `${cmd} -m pip install -r requirements.txt`,
                (err2, stdout2, stderr2) => {
                    if (err2) {
                        console.error(
                            "Erro ao instalar dependências Python:",
                            stderr2
                        );
                        process.exit(1);
                    }
                    console.log("Dependências Python instaladas com sucesso!");
                }
            );
        }
    });
}

console.log("Instalando dependências Python via requirements.txt...");
installWithPythonCmd("python");
