# Projeto Introdução ao Unity — rubrica e critérios verificáveis

Fonte normativa: `docs/Projeto_IntroUnity_Original.pdf` (4 páginas). Material do tutorial: `docs/Tutorial_Introducao_Rapida_Original.pdf` (13 páginas). Em caso de conflito, os PDFs oficiais prevalecem. **Não presumir política de IA da disciplina.**

**Tipo:** Individual. **Entrega:** 20/10/2026, 23h50 (conforme o PDF). **Jam:** https://itch.io/jam/projeto-intro-unity-insper-20262

## Requisitos e pontos do PDF

| ID | Critério do PDF | Pontos | Evidência mínima de validação |
|---|---|---:|---|
| 1 | Tutorial Introdução Rápida | 400 | Menu, cena de jogo, jogador, coleta, áudio e final de partida funcionando |
| 2.1.1 | Adicionar um cronômetro | parte de 200 | Tempo atualizado durante gameplay |
| 2.1.2 | Mostrar o tempo final ao término da partida | parte de 200 | Resultado mostra tempo decorrido após vitória e derrota |
| 2.1.3 | Usar tempo como mecânica dentro do jogo | parte de 200 | Tempo esgotado causa derrota, exibindo explicação e opção de tentar novamente |
| 2.2.1 | Inimigos com movimento predefinido/impacto, seguimento ou comportamento variável | 150 | Pelo menos um patrulha e um persegue; dano observável, testado em runtime |
| 2.3.1 | Trocar cor dos sprites básicos | parte de 75 | Comparação da cena e sprites antes/depois |
| 2.3.2 | Usar assets de sprites de forma coerente | parte de 75 | Estilo visual consistente e assets corretamente importados/creditados |
| 2.4.1 | Adicionar música ao jogo | parte de 100 | Música toca em runtime com nível adequado |
| 2.4.2 | Adicionar diferentes elementos sonoros | parte de 100 | Sons distintos para coleta, dano e resultado |
| 2.5.1 | Score e tempo atual na interface durante o jogo | parte de 100 | HUD legível exibe ambos e atualiza corretamente |
| 2.5.2 | Outras informações importantes (vida/stamina etc.) | parte de 100 | HUD informa vidas (ou equivalente) |
| 2.6.1 | Suporte a joystick/controle Xbox | 50 | Movimento testado com gamepad, além do teclado; WebGL checado se houver hardware |
| 2.7.1 | Ajustar jogo a um tema | parte de 150 | Cenário, HUD, áudio e objetos coerentes com o tema |
| 2.7.2 | Criar nova arena ou modificar a arena básica | parte de 150 | Layout modificado, jogável e visualmente distinto do tutorial |
| 2.7.3 | Adicionar novos desafios | parte de 150 | Obstáculos, patrulhas/perseguição, pressão de tempo sem travar o jogador |

**Total de pontos extras listados:** 825. **Total listado com tutorial:** 1.225. **Inconsistência no PDF:** fórmula da página 4 é `N(x) = (400 + x) / 1625 * 10`, com denominador 1.625, diferente do total 1.225. Não prometer nota 10 nem reinterpretar a fórmula sem confirmação do docente.

## Penalidades a impedir (prioridade máxima)

- Sem menu inicial: -500. Menu incoerente: -100.
- Sem tratamento de dead-end/softlock: -500.
- Página itch.io sem capa, descrição, nome ou link do GitHub: -100 por item.
- Atraso: -200 por dia (a seção de regras também fala em 2 pontos/dia; registrar a inconsistência, não assumir desconto pequeno).
- Repositório não público: -500.
- Não jogar em WebGL pelo navegador: -500.
- Plágio: -2000. Creditar assets e código de terceiros e observar integridade intelectual.
- Entrega incompleta no Blackboard: conforme regras, trabalho não é considerado entregue.

## Entrega obrigatória

1. GitHub **público**, projeto Unity com `.meta`, `Assets/`, `Packages/`, `ProjectSettings/`; `README.md` com instruções, controles, referências e créditos/licenças.
2. Build WebGL **jogável em navegador**; criar página do itch.io dentro da jam, com nome completo do aluno, capa, descrição, instruções para jogar e link do repo.
3. Registrar entrega no Blackboard. A publicação externa e o Blackboard precisam de confirmação humana; não alegar que estão feitos se não houver evidência.
4. Validar em execução real, não apenas compilação; conferir erros do Console, cenas, componentes, prefabs, serialização, inputs e áudio.

## Evidências e estado

Use `[ ] não feito`, `[~] implementado mas não validado`, `[x] testado com evidência`, `[!] bloqueado`. Atualize este quadro durante a execução. Não declarar todos os itens realizados apenas porque os scripts existem.
