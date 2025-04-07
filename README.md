# Bitstamp Simulador de Ordens

Este projeto implementa um microserviço em .NET 6 com foco em ingestão de dados em tempo real, persistência em banco NoSQL e exposição de uma API para simulação de melhores preços de operações. A proposta segue rigorosamente os princípios DDD, SOLID, DRY, KISS e YAGNI.

---

## 🚀 Funcionalidades

- Conexão via WebSocket com Bitstamp para os ativos **BTC/USD** e **ETH/USD**
- Armazenamento dos snapshots em **MongoDB**, separados por ativo
- Exibição de estatísticas a cada 5 segundos no console:
  - Maior e menor preço atual
  - Média de preço atual
  - Média de preço e quantidade acumuladas (5s)
- API REST para simulação de operação de compra/venda
- Testes automatizados com cobertura superior a **80%**

---

## 🧰 Tecnologias Utilizadas

- C# (.NET 6, ASP.NET Core)
- WebSockets
- MongoDB (via driver oficial)
- xUnit, Moq (testes)
- Swagger (documentação interativa da API)

---

## 📆 Arquitetura Aplicada

- **DDD (Domain-Driven Design)**: separação entre domain, application, infrastructure e presentation
- **SOLID**: interfaces, injeção de dependência, responsabilidade única
- **KISS / DRY / YAGNI**: código limpo, simples e direto ao ponto

---

## 💼 Como Executar o Projeto

### Requisitos:
- .NET 6 SDK
- MongoDB rodando localmente (porta padrão 27017)


---

## 📈 Simulação de Operação

### Endpoint:
```http
POST /simulacao
```

### Corpo da requisição:
```json
{
  "ativo": "BTCUSD",
  "tipo": "compra",
  "quantidade": 1.5
}
```

### Retorno:
```json
{
  "ativo": "BTCUSD",
  "tipo": "compra",
  "quantidade": 1.5,
  "total": 15000.00,
  "precoMedio": 10000.00
}
```

A API percorre as ordens (asks/bids) até completar a quantidade e retorna o total e o preço médio.

---

## 📚 Testes Automatizados

- Framework: xUnit + Moq
- Cobertura: 80%+

---

## 📄 Diferenciais do Projeto

- Conectividade com WebSocket (cliente próprio com reconexão)
- Camadas bem separadas (DDD)
- Fácil manutenção e extensão (novos ativos, novas APIs)
- API funcional e bem documentada via Swagger
- Alta cobertura de testes automatizados
- Pronto para execução em ambientes Linux (AKS, contêineres)

---

> Projeto desenvolvido como parte de um desafio técnico. Qualquer dúvida ou sugestão, entre em contato!

