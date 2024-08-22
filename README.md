# Alerta - Códigos do Projeto

Este repositório contém os códigos-fonte do projeto **Alerta**, um jogo educativo desenvolvido em Unity para ensinar os jogadores a identificar e-mails de phishing e melhorar a conscientização sobre cibersegurança.

## Visão Geral

O projeto **Alerta** é um jogo que combina mecânicas interativas com um conteúdo educativo focado em cibersegurança. Os jogadores interagem com e-mails simulados para aprender a identificar ameaças de phishing. Este repositório inclui todos os scripts e códigos utilizados no desenvolvimento do jogo.

## Estrutura do Repositório

- **EmailManager.cs**: Gerencia a exibição e o comportamento dos e-mails no jogo. Inclui funcionalidades como:
  - Exibição de um painel pop-up para feedback de e-mails marcados como corretos ou incorretos.
  - Controle de tentativas para cada e-mail (até duas tentativas por e-mail).
  - Salvamento dos resultados da fase em um arquivo de texto.

- **PhaseConfig.cs**: Define a configuração de cada fase do jogo, incluindo:
  - Atribuição dos e-mails que serão apresentados em cada fase.
  - Definição dos personagens envolvidos em cada fase.
  - Configuração das condições de término da fase, como o número de e-mails a serem processados e as ações de feedback.

- **Managers para Cenas Específicas**:
  - **FinaltestManager.cs**: Gerencia a fase final do jogo, onde o jogador passa por um teste final. Controla o comportamento do botão `nextPhaseButton`, que é desativado nesta fase, e a transição para cenas com base na pontuação obtida.
  - **HiredManager.cs**: Controla a cena final onde o jogador é "contratado" pela JR Company após concluir todas as fases com sucesso. Configura a exibição de mensagens e feedback final ao jogador.
  - **Outros Managers**: Gerenciam outras cenas específicas do jogo, garantindo que as transições, feedbacks e interações ocorram de forma adequada em cada etapa do jogo.

- **Personagens e Fases**:
  - Código que define a interação com os personagens Matheus, Hannah, Lucas e Joana, cada um aparecendo em fases específicas do jogo. Matheus, por exemplo, atua como o chefe do RH da JR Company e guia o jogador durante o processo seletivo.

- **Detecção de Phishing**:
  - Código para permitir que o jogador marque e-mails como phishing ou legítimos, com pop-ups de feedback para as escolhas corretas ou incorretas.

- **Sistema de Fases**:
  - Implementação do sistema de fases, incluindo o botão para finalizar a fase e o sistema de feedback de consequências para erros de identificação.

- **Finalização do Jogo**:
  - Código para desativar o botão `nextPhaseButton` no Level 5 e usar outro botão para direcionar o jogador para a cena correspondente com base na pontuação.

## Tecnologias Utilizadas

- **Unity 3D**: Ferramenta principal usada para o desenvolvimento do jogo.
- **C#**: Linguagem de programação utilizada para escrever os scripts.

## Repositório da Versão Build

Você pode acessar o repositório contendo a versão compilada do jogo para PC no seguinte link:

[Alerta - Build para PC](https://github.com/JassonJr1/alerta_prototype-ver1-/tree/main)

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](./LICENSE) para mais detalhes.
