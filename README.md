# 📉 SimuladorCripto.BitStamp – Consulta e Análise de Preços Cripto via API

Sistema em C# que **consome dados em tempo real da API pública da BitStamp** e simula preços de criptomoedas como BTC/USD.  
Ideal para estudos de integração com **APIs financeiras**, testes de lógica de bots de análise, e coleta de dados para estratégias futuras.

> 💡 Projeto leve e real para mostrar domínio técnico em APIs REST, JSON parsing e simulações com base em dados de mercado.

<!-- 🔷 IMAGEM DE CAPA (opcional)
Ex: um print do console rodando ou um mockup com o logo da BitStamp
Substituir abaixo:
![Banner ou preview](imagens/banner-simulador.png)
-->

---

## 🧠 O que esse projeto faz?

- Consulta o preço atual do par **BTC/USD** via `https://www.bitstamp.net/api/ticker/`
- Lê os dados em JSON (último preço, bid, ask, etc.)
- Exibe os resultados no console
- Serve como base para futuros bots de trading, notificações, backtesting ou relatórios financeiros

<!-- 🔷 FLUXOGRAMA DO FUNCIONAMENTO (opcional)
Exemplo de diagrama de blocos:
[Input API] ➜ [Service HTTP] ➜ [Parse JSON] ➜ [Exibe Resultado]
Adicionar imagem:
![Fluxograma](imagens/fluxo-bitstamp.png)
-->

---

## 🚀 Tecnologias utilizadas

- ✅ **C# (.NET)**
- ✅ **HttpClient** (para consumir a API)
- ✅ **JSON Parsing**
- ✅ **Console Application**
- *(fácil de estender para CSV, banco de dados ou dashboards)*

---

## 📦 Estrutura Sugerida do Projeto

SimuladorCripto.BitStamp/
├── src/
│   ├── BitStampPrecoService.cs
│   ├── Program.cs
│   └── Simulador.cs
├── imagens/
│   └── exemplo-console.png
├── README.md

---

## 📸 Exemplo de Retorno da API

{
  "high": "68943.00",
  "last": "68012.00",
  "timestamp": "1716679416",
  "bid": "68011.99",
  "ask": "68016.00"
}

<!-- 🔷 PRINT DO CONSOLE (opcional)
Exibir o resultado real no console C# após consumir a API.
Substituir abaixo:
![Exemplo Console](imagens/exemplo-console.png)
-->

---

## 💡 Possibilidades de Expansão

- 🔔 Alerta por e-mail ou Telegram ao atingir determinado preço
- 📊 Exportar para CSV, banco de dados ou relatório HTML
- 🧠 Adicionar lógica de compra/venda simulada
- 🤖 Integrar com bots de análise e tomada de decisão

---

## 📈 Aplicações Reais

- Estudos de mercado cripto (BTC/USD)
- Testes de integração REST com C#
- Simulação de estratégias ou regras de negócio
- Demonstração para projetos de automação e análise de dados

---

## 🧩 Palavras-chave para SEO

`csharp` • `rest api` • `bitstamp` • `bitcoin` • `api cripto` • `automação` • `bot` • `simulador de preços` • `trading bot` • `json` • `monitoramento`

---

## 📫 Contato

📧 Email: leandrofarias.dev@gmail.com  
💼 LinkedIn: [linkedin.com/in/leandrofarias-dev](https://linkedin.com/in/leandrofarias-dev)  
🔗 GitHub: [github.com/lesistemas](https://github.com/lesistemas)  
📍 Local: Brasil — disponível para projetos remotos

---

<!-- 🧩 RODAPÉ COM BADGES -->
![C#](https://img.shields.io/badge/-C%23-239120?style=flat&logo=csharp&logoColor=white)
![REST API](https://img.shields.io/badge/-REST%20API-black?style=flat&logo=api)
![Bitcoin](https://img.shields.io/badge/-Bitcoin-F7931A?style=flat&logo=bitcoin&logoColor=white)
![BitStamp](https://img.shields.io/badge/-BitStamp-006400?style=flat&logo=data:image/svg+xml;base64,<fake>)
