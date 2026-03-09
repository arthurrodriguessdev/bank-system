# Sistema Bancário Console em C#

## Descrição do Projeto
Este é um sistema de **gestão bancária em console**, desenvolvido em C#. O objetivo do sistema é permitir o cadastro de clientes, criação de contas correntes e poupança, e realizar operações financeiras básicas como saques, depósitos e aplicação de juros ou limites.

O projeto aplica conceitos de **Orientação a Objetos**, herança, polimorfismo, encapsulamento e boas práticas de código.

---

## Funcionalidades Principais do Sistema

- Cadastro de clientes (Nome, Sobrenome, CPF, Data de Nascimento)  
- Cadastro de contas correntes e poupança vinculadas a clientes  
- Saque com validação de saldo e limite (Conta Corrente)  
- Depósito com validação de valores  
- Aplicação de juros automáticos em contas poupança  
- Interface de menu interativo em console  
- Visualização de clientes cadastrados e suas contas  
- Prevenção de duplicidade de contas ou clientes  

---

## Tecnologias Utilizadas

**Back-End**

- C#  
- .NET (Console Application)  

**Conceitos Aplicados**

- Orientação a Objetos (Classes, Herança, Polimorfismo)  
- Encapsulamento e propriedades calculadas  
- Coleções 
- Tipos dinâmicos e uso de dicionários

---

## Arquitetura do Sistema

- **Separação de responsabilidades:**  
  - `Model` → Classes de domínio (`Cliente`, `Conta`, `ContaCorrente`, `ContaPoupanca`)  
  - `Services` → Lógica de controle, menus e operações  
  - `Utils` → Métodos auxiliares, interface e buscas genéricas  

- **OOP aplicada corretamente:**  
  - Herança entre contas  
  - Métodos virtuais e sobrescritos  
  - Propriedades calculadas para idade, limite e juros  

- **Validações e segurança:**  
  - Evita duplicidade de clientes e contas  
  - Validação de entradas de usuário  
  - Mensagens claras no console  

---

## Licença
- Uso comercial não permitido
- Código disponível apenas para visualização

Todos os direitos reservados ao autor.
---

## Autor
**Arthur Rodrigues**
Desenvolvedor Back-End

LinkeIn: www.linkedin.com/in/arthur-rodriguesx

## Instruções de Uso

1. Clone o repositório:  
```bash
git clone https://github.com/seuusuario/seuprojeto.git
