# Projeto CardioCheck: Aplicação Mobile para Verificação de Risco de Doença Cardíaca

## Memorial Descritivo do Projeto

### Visão Geral
O CardioCheck é uma aplicação mobile para dispositivos Android que utiliza inteligência artificial para avaliar o risco de doenças cardíacas com base em dados fornecidos pelo usuário. A aplicação coleta informações como sintomas, resultados de exames, frequência cardíaca, idade e sexo, processando-os através de um modelo de classificação para determinar a probabilidade de presença de doença cardíaca.

### Arquitetura do Sistema
O sistema é composto por quatro componentes principais:
- **Aplicação Mobile (Frontend):** Desenvolvida em C# utilizando o framework .NET MAUI para garantir uma interface responsiva e intuitiva para dispositivos Android.
- **API (Backend):** Implementada em Node.js com Express, responsável por gerenciar as requisições do aplicativo, autenticação de usuários e comunicação com o modelo de IA.
- **Servidor:** Hospedado na Azure em uma VM Ubuntu Server, garantindo escalabilidade, segurança e alta disponibilidade.
- **Modelo de IA:** Desenvolvido em Python, utiliza algoritmos de aprendizado de máquina para classificar o risco de doença cardíaca com base nos dados fornecidos pelo usuário.

### Fluxo de Dados
- O usuário se cadastra e faz login no aplicativo
- O usuário preenche o questionário com seus dados de saúde
- Os dados são enviados para a API
- A API encaminha os dados para o modelo de IA
- O modelo processa os dados e retorna uma classificação (0 - Normal, 1 - Risco de doença cardíaca)
- O resultado é armazenado no banco de dados e exibido para o usuário
- Recomendações são fornecidas com base no resultado

### Análise de Requisitos

### 1. Requisitos Funcionais
**RF01 - Cadastro de Usuário**
- O sistema deve permitir o cadastro de novos usuários coletando: Nome, Idade, Sexo, Email, Senha, Endereço, Telefone e CPF
- O sistema deve validar a unicidade do CPF e email
- O sistema deve criptografar a senha do usuário antes de armazená-la

**RF02 - Autenticação de Usuário**
- O sistema deve permitir login utilizando CPF ou Email e Senha
- O sistema deve emitir e validar tokens JWT para manter a sessão do usuário
- O sistema deve permitir a recuperação de senha via email

**RF03 - Questionário de Saúde**
- O sistema deve apresentar um questionário com 9 perguntas relacionadas a fatores de risco para doenças cardíacas
- O sistema deve coletar e validar os seguintes dados:
   - Tipo de dor no peito (numérico)
   - Pressão arterial em repouso (numérico)
   - Colesterol sérico em mg/dl (numérico)
   - Glicemia de jejum > 120 mg/dl (booleano)
   - Resultados de eletrocardiograma em repouso (categórico)
   - Frequência cardíaca máxima atingida (numérico)
   - Angina induzida por exercício (booleano)
   - Depressão do segmento ST induzida por exercício (numérico)
   - Inclinação do segmento ST de pico do exercício (categórico)

**RF04 - Processamento e Classificação**
- O sistema deve enviar os dados coletados para processamento pelo modelo de IA
- O sistema deve classificar o risco de doença cardíaca (0 - Normal, 1 - Risco de doença)
- O sistema deve gerar recomendações personalizadas com base no resultado

**RF05 - Visualização de Resultados**
- O sistema deve exibir o resultado da classificação de forma clara e compreensível
- O sistema deve apresentar recomendações médicas baseadas no resultado
- O sistema deve indicar a necessidade de procurar um especialista quando apropriado

**RF06 - Histórico de Avaliações**
- O sistema deve armazenar o histórico de todas as avaliações realizadas pelo usuário
- O sistema deve permitir a visualização do histórico em formato de cards, exibindo data e resultado
- O sistema deve permitir a exclusão de registros do histórico

### 2. Requisitos Não Funcionais
**RNF01 - Desempenho**
- O tempo de resposta para processamento do questionário não deve exceder 3 segundos
- A aplicação deve suportar até 1000 usuários simultâneos
- O sistema deve processar até 10.000 avaliações por dia

**RNF02 - Segurança**
- Todos os dados pessoais e médicos devem ser criptografados em trânsito e em repouso
- O sistema deve implementar autenticação de dois fatores
- O sistema deve estar em conformidade com a LGPD (Lei Geral de Proteção de Dados)
- O sistema deve realizar backups diários dos dados

**RNF03 - Usabilidade**
- A interface deve ser intuitiva e acessível para usuários de todas as idades
- O aplicativo deve seguir as diretrizes de design do Material Design
- O sistema deve fornecer feedback claro sobre erros e ações realizadas

**RNF04 - Disponibilidade**
- O sistema deve estar disponível 99,9% do tempo (downtime máximo de 8,76 horas por ano)
- Manutenções programadas devem ser realizadas em horários de baixo uso

**RNF05 - Escalabilidade**
- A arquitetura deve permitir escalar horizontalmente para atender ao aumento de demanda
- O banco de dados deve suportar crescimento de até 1TB por ano

**RNF06 - Compatibilidade**
- O aplicativo deve ser compatível com Android 8.0 (Oreo) ou superior
- A interface deve se adaptar a diferentes tamanhos de tela

### Regras de Negócio
**RN01 - Cadastro e Acesso**
- Apenas usuários maiores de 18 anos podem se cadastrar no sistema
- Um CPF só pode estar associado a uma única conta
- Um email só pode estar associado a uma única conta

**RN02 - Avaliação de Risco**
- O modelo de IA deve ser treinado com o dataset do Kaggle especificado
- A precisão mínima aceitável do modelo de classificação é de 85%
- O modelo deve ser atualizado trimestralmente com novos dados

**RN03 - Recomendações Médicas**
- Usuários classificados com risco de doença cardíaca devem receber recomendação para consultar um cardiologista em até 30 dias
- Usuários classificados como normais devem receber recomendações preventivas
- As recomendações devem ser revisadas por profissionais de saúde qualificados

**RN04 - Privacidade e Ética**
- Os dados dos usuários não podem ser compartilhados com terceiros sem consentimento explícito
- O sistema deve apresentar termos de uso e política de privacidade claros
- O usuário deve poder solicitar a exclusão completa de seus dados

**RN05 - Limitações e Avisos**
- O sistema deve exibir claramente que não substitui diagnóstico médico profissional
- Resultados críticos devem ser acompanhados de avisos sobre a urgência de buscar atendimento médico

### Justificativa
As doenças cardiovasculares são a principal causa de morte no mundo, sendo responsáveis por aproximadamente 17,9 milhões de óbitos anualmente, segundo a Organização Mundial da Saúde. A detecção precoce de fatores de risco e a identificação de sinais de alerta podem salvar vidas e reduzir custos com tratamentos intensivos.
O CardioCheck visa democratizar o acesso a uma ferramenta de triagem preliminar que pode alertar usuários sobre potenciais riscos cardíacos, incentivando-os a buscar atendimento médico especializado quando necessário. A aplicação não substitui o diagnóstico médico, mas serve como um primeiro passo para conscientização e prevenção.
A escolha das tecnologias (.NET MAUI, Node.js, Azure e Python) permite o desenvolvimento de uma solução robusta, escalável e segura, capaz de processar dados sensíveis de saúde com a devida proteção e confiabilidade. O uso de inteligência artificial possibilita a análise de múltiplos fatores de risco simultaneamente, oferecendo uma avaliação mais abrangente do que métodos tradicionais de triagem.

### Diagrama de Classes do Banco de Dados
+-------------------+       +----------------------+       +-------------------+
|      Usuario      |       |      Avaliacao       |       |   Recomendacao    |
+-------------------+       +----------------------+       +-------------------+
| id: UUID          |       | id: UUID             |       | id: UUID          |
| nome: String      |       | usuario_id: UUID     |<----->| avaliacao_id: UUID|
| idade: Integer    |       | data: DateTime       |       | texto: Text       |
| sexo: Enum        |<----->| resultado: Boolean   |       | tipo: Enum        |
| email: String     |       | pressao: Float       |       | criado_em: DateTime|
| senha: String     |       | colesterol: Float    |       +-------------------+
| endereco: String  |       | glicemia: Boolean    |
| telefone: String  |       | ecg_repouso: Enum    |
| cpf: String       |       | freq_cardiaca: Integer|
| criado_em: DateTime|      | angina: Boolean      |
| atualizado_em: DateTime|  | depressao_st: Float  |
+-------------------+       | inclinacao_st: Enum  |
                            | num_vasos: Integer   |
                            | criado_em: DateTime  |
                            +----------------------+


### BPMN (Business Process Model and Notation)
### 1. Processo Principal do CardioCheck
[Início] --> [Cadastro de Usuário] --> [Login] --> [Preenchimento do Questionário] 
--> [Envio para Processamento] --> [Análise pelo Modelo de IA] 
--> [Geração de Resultado] --> [Exibição do Resultado e Recomendações] 
--> [Armazenamento no Histórico] --> [Fim]

### 2. Subprocesso: Cadastro de Usuário
[Início] --> [Preenchimento de Dados Pessoais] --> [Validação de Dados] 
--> <Dados Válidos?> 
   --> [Sim] --> [Criação de Conta] --> [Envio de Email de Confirmação] --> [Fim]
   --> [Não] --> [Exibição de Erros] --> [Preenchimento de Dados Pessoais]

### 3. Subprocesso: Preenchimento do Questionário
[Início] --> [Exibição das Perguntas] --> [Coleta de Respostas] 
--> [Validação das Respostas] --> <Respostas Completas?> 
   --> [Sim] --> [Confirmação dos Dados] --> [Fim]
   --> [Não] --> [Exibição de Campos Pendentes] --> [Coleta de Respostas]

### 4. Subprocesso: Análise pelo Modelo de IA
[Início] --> [Recebimento dos Dados] --> [Pré-processamento] 
--> [Aplicação do Modelo de Classificação] --> [Cálculo de Probabilidade] 
--> [Determinação do Resultado Final] --> [Geração de Recomendações] --> [Fim]

### Detalhamento das Telas
1. Tela de Cadastro
- Campos para entrada de dados pessoais (Nome, Idade, Sexo, Email, Senha, Endereço, Telefone, CPF)
- Validação em tempo real dos campos
- Botão para enviar cadastro
- Link para tela de login (caso já tenha conta)

2. Tela de Login
- Campo para CPF ou Email
- Campo para Senha
- Opção "Lembrar-me"
- Link para recuperação de senha
- Botão para entrar
- Link para tela de cadastro

3. Tela de Questionário
- Formulário com 9 perguntas sobre fatores de risco cardíaco
- Campos adequados para cada tipo de dado (numérico, booleano, categórico)
- Explicações sobre cada pergunta
- Botão para enviar respostas
- Botão para limpar formulário

4. Tela de Resultados
- Indicação visual clara do resultado (0 - Normal, 1 - Risco de doença)
- Explicação detalhada do significado do resultado
- Recomendações personalizadas baseadas no resultado
- Indicação para procurar especialista (quando necessário)
- Botão para salvar ou compartilhar resultado
- Botão para realizar nova avaliação

5. Tela de Histórico
- Lista de cards mostrando avaliações anteriores
- Cada card contém data e resultado (positivo/negativo)
- Opção para filtrar por período
- Opção para ordenar por data
- Funcionalidade para excluir registros
- Gráfico de tendência (opcional)

### Conclusão
O CardioCheck representa uma solução inovadora para triagem preliminar de riscos cardíacos, utilizando tecnologias modernas e inteligência artificial para fornecer uma avaliação acessível e rápida. A aplicação tem o potencial de contribuir significativamente para a conscientização sobre saúde cardíaca e incentivar a busca por atendimento médico preventivo.
A arquitetura proposta garante segurança, escalabilidade e usabilidade, enquanto o modelo de IA treinado com dados reais proporciona resultados confiáveis. O projeto segue as melhores práticas de desenvolvimento ágil, permitindo iterações e melhorias contínuas com base no feedback dos usuários e avanços na área médica.


Atualização do Projeto CardioCheck: Detalhamento do Questionário de Saúde
Questionário de Saúde Atualizado (RF03)
Com base nas informações fornecidas, vou detalhar o questionário de saúde que será implementado no aplicativo CardioCheck. Este questionário coletará dados específicos que serão utilizados pelo modelo de IA para classificar o risco de doença cardíaca.
RF03 - Questionário de Saúde
O sistema deve apresentar um questionário com 9 perguntas relacionadas a fatores de risco para doenças cardíacas, coletando e validando os seguintes dados:

Tipo de Dor no Peito (Chest Pain Type)

Tipo: Nominal (categórico)
Valores possíveis: 1, 2, 3, 4
Interface: Botões de seleção única ou dropdown
Descrição para o usuário:
1: Angina típica
2: Angina atípica
3: Dor não-anginosa
4: Assintomático




Pressão Arterial em Repouso (Resting Blood Pressure)

Tipo: Numérico
Unidade: mm Hg
Faixa típica: 90-200 mm Hg
Interface: Campo numérico com validação
Descrição: "Informe sua pressão arterial sistólica em repouso (número mais alto da medição)"


Colesterol Sérico (Serum Cholesterol)

Tipo: Numérico
Unidade: mg/dl
Faixa típica: 100-600 mg/dl
Interface: Campo numérico com validação
Descrição: "Informe seu nível de colesterol total em mg/dl"


Glicemia de Jejum (Fasting Blood Sugar)

Tipo: Binário
Valores: 0 (≤ 120 mg/dl), 1 (> 120 mg/dl)
Interface: Botões de seleção ou toggle switch
Descrição: "Sua glicemia de jejum é maior que 120 mg/dl?"


Resultados de Eletrocardiograma em Repouso (Resting Electrocardiogram Results)

Tipo: Nominal (categórico)
Valores: 0, 1, 2
Interface: Botões de seleção única ou dropdown
Descrição para o usuário:
0: Normal
1: Anormalidade da onda ST-T
2: Hipertrofia ventricular esquerda provável ou definitiva




Frequência Cardíaca Máxima Atingida (Maximum Heart Rate Achieved)

Tipo: Numérico
Faixa: 71-202 bpm
Interface: Campo numérico com validação
Descrição: "Informe sua frequência cardíaca máxima atingida durante exercício"


Angina Induzida por Exercício (Exercise Induced Angina)

Tipo: Binário
Valores: 0 (Não), 1 (Sim)
Interface: Botões de seleção ou toggle switch
Descrição: "Você sente dor no peito (angina) durante exercícios físicos?"


Depressão do Segmento ST (Oldpeak)

Tipo: Numérico
Descrição: Depressão do segmento ST induzida por exercício em relação ao repouso
Faixa típica: 0-6.2
Interface: Campo numérico com validação
Descrição: "Informe o valor da depressão do segmento ST (conforme resultado do teste de esforço)"


Inclinação do Segmento ST (ST Slope)

Tipo: Nominal (categórico)
Valores: 0, 1, 2
Interface: Botões de seleção única ou dropdown
Descrição para o usuário:
0: Ascendente
1: Plano
2: Descendente





Design da Interface do Questionário
Considerações de Design

Acessibilidade:

Texto claro e legível
Alto contraste para facilitar a leitura
Suporte a leitores de tela
Tamanho de fonte ajustável


Usabilidade:

Perguntas apresentadas uma por vez ou em grupos lógicos
Barra de progresso para indicar avanço no questionário
Botões "Anterior" e "Próximo" para navegação
Validação em tempo real dos dados inseridos


Informações Auxiliares:

Ícone de informação (i) ao lado de cada pergunta que, quando tocado, exibe explicação detalhada
Glossário de termos médicos acessível durante o preenchimento
Exemplos visuais para auxiliar na compreensão (ex: imagens ilustrativas de diferentes tipos de dor no peito)



Layout Proposto
+------------------------------------------+
|                CardioCheck               |
|                                          |
| Questionário de Saúde Cardíaca (3/9)     |
| [==========>                    ]        |
+------------------------------------------+
|                                          |
| Colesterol Sérico                     (i)|
|                                          |
| Informe seu nível de colesterol total    |
| em mg/dl (conforme seu exame mais        |
| recente)                                 |
|                                          |
| +----------------------------+           |
| |                      mg/dl|           |
| +----------------------------+           |
|                                          |
| Faixa típica: 100-600 mg/dl              |
|                                          |
|                                          |
| [Anterior]                    [Próximo]  |
|                                          |
| [Salvar e continuar depois]              |
+------------------------------------------+

Validações e Regras de Negócio Específicas
Validações Técnicas

Tipo de Dor no Peito:

Validação: Apenas valores 1, 2, 3 ou 4 são aceitos
Obrigatório: Sim


Pressão Arterial em Repouso:

Validação: Valor numérico entre 90 e 200 mm Hg
Alerta se < 90 ou > 180 mm Hg
Obrigatório: Sim


Colesterol Sérico:

Validação: Valor numérico entre 100 e 600 mg/dl
Alerta se < 130 ou > 240 mg/dl
Obrigatório: Sim


Glicemia de Jejum:

Validação: Apenas valores 0 ou 1 são aceitos
Obrigatório: Sim


Resultados de Eletrocardiograma em Repouso:

Validação: Apenas valores 0, 1 ou 2 são aceitos
Obrigatório: Sim


Frequência Cardíaca Máxima Atingida:

Validação: Valor numérico entre 71 e 202 bpm
Alerta se valor for inconsistente com a idade (220 - idade)
Obrigatório: Sim


Angina Induzida por Exercício:

Validação: Apenas valores 0 ou 1 são aceitos
Obrigatório: Sim


Depressão do Segmento ST:

Validação: Valor numérico entre 0 e 6.2
Obrigatório: Sim


Inclinação do Segmento ST:

Validação: Apenas valores 0, 1 ou 2 são aceitos
Obrigatório: Sim



Regras de Negócio Específicas

RN-Q01: Se o usuário não possuir algum dos dados solicitados, o sistema deve oferecer a opção de salvar o questionário parcialmente preenchido para continuação posterior.

RN-Q02: O sistema deve fornecer explicações claras sobre como obter os dados médicos solicitados (ex: "Este dado pode ser encontrado em seu exame de sangue recente").

RN-Q03: Para valores extremos (outliers), o sistema deve solicitar confirmação do usuário antes de prosseguir.

RN-Q04: O sistema deve permitir que o usuário anexe resultados de exames (opcional) para referência futura.

RN-Q05: O sistema deve alertar o usuário sobre a importância de fornecer dados precisos para a acurácia da avaliação.


Fluxo de Preenchimento do Questionário

O usuário acessa a tela do questionário após login
O sistema apresenta uma introdução explicando o propósito e importância do questionário
O usuário navega pelas 9 perguntas, preenchendo os dados solicitados
O sistema valida cada entrada em tempo real, fornecendo feedback imediato
Ao final do preenchimento, o sistema apresenta um resumo dos dados informados
O usuário confirma os dados ou retorna para editar
Após confirmação, os dados são enviados para processamento pelo modelo de IA
O sistema exibe uma tela de carregamento enquanto processa os dados
O resultado da avaliação é apresentado na tela de resultados

Considerações Técnicas para Implementação

Armazenamento de Dados:

Os dados do questionário devem ser armazenados de forma criptografada
A estrutura do banco de dados deve permitir o versionamento dos questionários (para caso de mudanças futuras no modelo)


Integração com o Modelo de IA:

Os dados coletados devem ser normalizados antes de serem enviados ao modelo
O formato de envio deve ser compatível com a API do modelo Python


Experiência Offline:

O aplicativo deve permitir o preenchimento do questionário offline
Os dados devem ser sincronizados quando a conexão for restabelecida


Acessibilidade:

Implementar suporte a TalkBack para Android
Garantir que todos os elementos sejam navegáveis por teclado
Seguir diretrizes WCAG 2.1 nível AA



Esta especificação detalhada do questionário de saúde garante que o aplicativo CardioCheck colete todos os dados necessários para uma avaliação precisa do risco de doença cardíaca, seguindo as melhores práticas de design de interface e experiência do usuário.
