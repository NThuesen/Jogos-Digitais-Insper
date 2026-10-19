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

## Estado da implementação — 19/10/2026

Detalhes e limites dos testes em [GATES_CRYSTAL_ESCAPE.md](GATES_CRYSTAL_ESCAPE.md); transcrição em [RUBRICA_MCP.jsonl](RUBRICA_MCP.jsonl).

| Critério | Estado | Evidência real |
|---|---|---|
| 1 Tutorial | [x] | Estabilização validada por MCP; fluxo estendido preserva menu, Rigidbody2D, prefabs, quatro triggers, áudio, resultado e reinício. |
| 2.1.1 Cronômetro | [x] | HUD inicia em 90s e atualiza em Play Mode; teste de Tick com tempo conhecido. |
| 2.1.2 Tempo final | [x] | Capturas e valores de tempo final em vitória, timeout e derrota por vidas. |
| 2.1.3 Tempo como mecânica | [x] | Lógica e teste visual acelerado do Editor aprovados; WebGL também exibiu derrota após90s reais e reiniciou com90s. |
| 2.2.1 Inimigos | [x] | Patrulheiro movendo-se entre pontos; perseguidor reduziu distância de 2,91 para 0,79 e causou derrota por contato. Vitória com ambos ativos também validada. |
| 2.3.1 Cores | [x] | Comparação dos screenshots do baseline com menu/arena final em azul, ciano, roxo e coral. |
| 2.3.2 Sprites coerentes | [x] | Seis texturas originais procedurais aplicadas; inspeção de menu, jogador, cristais, drones, chão e reatores. |
| 2.4.1 Música | [x] | AudioSource em reprodução no menu e no jogo; loop de 20s. Percepção auditiva/volume ainda requer conferência humana. |
| 2.4.2 Sons diferentes | [x] | Collect, Hit, Victory e Defeat distintos; eventos do jogo e reprodução/estado dos AudioSources verificados via Editor. Conferência auditiva humana pendente. |
| 2.5.1 Score e tempo no HUD | [x] | Coletas: 100/200/300/400; HUD exibe tempo atual e resultado exibe tempo final. |
| 2.5.2 Vidas no HUD | [x] | Três danos aceitos, duplicados rejeitados; vidas 3→2→1→0 e painel de derrota. |
| 2.6.1 Xbox | [~] | Input System com analógico, direcional e Submit; deslocamento por gamepad sintético validado. Teste físico não realizado: aluno informou que não há controle disponível. WebGL com Xbox também não testado. |
| 2.7.1 Tema | [x] | Laboratório, protocolo de extração, cristais, drones, UI e áudio eletrônico procedurais. |
| 2.7.2 Arena | [x] | Dois reatores e novas posições dos objetivos; quatro caminhos acessíveis com raio 0,42; bloqueio de parede confirmado. |
| 2.7.3 Desafios | [x] | Tempo limite, patrulha, perseguição e dano; imunidade temporária e congelamento no fim testados. |

## Entrega e prevenção de penalidades

| Item | Estado | Limite / próxima ação |
|---|---|---|
| Menu coerente e funcional | [x] | Menu tem objetivo e controles; callbacks executados em Play Mode. |
| Prevenção de softlocks | [x] | Quatro objetivos acessíveis, paredes sólidas, três saídas de partida e botões de reinício/menu. Não é garantia contra todo bug possível. |
| Repositório público | [x] | Metadados reais do GitHub em GITHUB_STATUS.json. Publicar o código local atualizado é etapa separada. |
| Build WebGL de segurança | [x] | Builds/WebGL-Safety; sucesso, zero erros; menu aberto no Edge headless. |
| Build WebGL final | [x] | Relatório nativo Succeeded/WebGL,zero erros; duas vitórias,menu,nova partida,timeout90s e restart no Edge. ZIP CRC íntegro, index.html na raiz. Avisos em VALIDACAO_FINAL.md. |
| Capa e descrição do itch.io | [~] | Material local pronto em Builds/Release e ITCH_IO_TEXTO.md; página externa não publicada. |
| Nome completo na página | [~] | Nicholas Thuesen informado e incluído no material local. Página externa ainda não publicada. |
| Link GitHub na página | [~] | URL real preparada no texto; página do itch.io ainda não publicada. |
| GitHub atualizado / itch.io / jam | [ ] | Publicações externas pendentes de confirmação humana. |
| Blackboard | [ ] | Enviar os links reais e conferir o comprovante antes de 20/10/2026, 23h50. |

Não há nota garantida; permanece a inconsistência de pontuação do PDF. Testes sintéticos de gamepad e de colisão automatizada não são classificados como teste físico de Xbox nem como avaliação auditiva humana.
