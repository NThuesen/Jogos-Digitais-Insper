# Tarefa do Codex — finalizar Projeto Introdução ao Unity

## Objetivo

Concluir o **projeto individual do tutorial**, e não iniciar um novo jogo. Implementar o menor conjunto coerente de melhorias que satisfaça o máximo da rubrica. Priorizar o funcionamento e a entrega, e evitar complexidade, regressões e mudanças desnecessárias.

## Fontes de verdade

1. `docs/Projeto_IntroUnity_Original.pdf` — enunciado oficial e regras de avaliação/entrega.
2. `docs/Tutorial_Introducao_Rapida_Original.pdf` — resumo dos 17 vídeos do tutorial.
3. `docs/RUBRICA_INTRO_UNITY.md` — extração operacional rastreável ao PDF.
4. Estado real **do repositório, cenas abertas e assets**. Não assumir que o código discutido em chat está idêntico ao do disco.

Em caso de divergência entre a extração e o PDF, registrar a divergência e respeitar o PDF. A proposta criativa abaixo não é requisito acadêmico.

## Baseline conhecido apenas por prints anteriores — confirmar no repo

- Projeto Unity 6, 2D; menu `MainMenu` no índice 0, cena do jogo `SampleScene` no índice 1 (verificar Scene List / Build Profiles).
- `PlayerMovement.cs`: Rigidbody2D, movimento 8 direções normalizado e áudio na coleta; tag `Coletavel`.
- `GameController.cs`: contador estático de coletáveis, `Init()` fixando 4, `Collect()` decrementando e `gameOver` quando chega a zero.
- `MenuActions.cs`: `IniciaJogo()` inicializa contagem e carrega cena 1; `Menu()` volta à cena 0.
- `UIManager.cs`: painel de vitória ativado ao encerrar coleta.
- Input Handling foi alterado para `Both` anteriormente, mas checar Player Settings do projeto real.
- Prefabs de Player e Triangle, áudio de coleta e paredes. Toda essa descrição é hipótese a confirmar, não prova do estado atual.

## Design mínimo proposto: Crystal Escape

Preservar a mecânica de coletar quatro itens. Reskin simples com tema de laboratório futurista/cristais; 1 arena renovada com obstáculos, 1 inimigo patrulheiro e 1 inimigo perseguidor, 3 vidas, score, tempo limite, música e SFX, tela de resultado e reinício. Não implementar inventário, loja, narrativa, progressão de múltiplas fases, multiplayer, save complexo, gerador procedural nem sistema extensível desnecessário.

### Regras de gameplay

- O jogador vence ao coletar todos os 4 cristais enquanto estiver vivo e com tempo positivo.
- O cronômetro regressivo dá pressão real: chegar a zero encerra partida com derrota; registrar e exibir **tempo final decorrido** também em caso de derrota. Valor inicial sugerido: 90 segundos; ajustar após testar.
- Cada cristal vale 100 pontos. Mostrar `Score`, `Cristais: N/4`, `Tempo: ...` e `Vidas: 3`/corações em HUD durante a partida.
- Inimigos têm Collider2D e produzem dano funcional; 1 patrulha entre pontos, 1 persegue ao entrar em alcance. Vida chega a 0 -> derrota; pequeno período de invulnerabilidade após dano evita múltiplas perdas instantâneas. Dar feedback visual e sonoro claro.
- Após vitória/derrota, mostrar resultado com score e tempo final, botões `Jogar novamente` e `Menu`. O tempo e os inputs de gameplay não devem continuar alterando estado após término.
- O menu inicial deve explicar controles e objetivo de maneira breve; botões funcionais. Se houver pausa, deve haver retorno/reinício; não adicionar pausa se gerar riscos de softlock.
- Evitar qualquer rota inevitavelmente bloqueada, coletável inalcançável, collider preso, reinício que retém estado antigo ou tela sem saída.
- Gamepad: preservar teclado; aceitar gamepad analógico/digital no Windows, testar com controle Xbox e WebGL se houver equipamento. Não afirmar suporte validado só porque há código do Input System. Qualquer migração de Input deve evitar quebrar UI e teclado.
- Assets: usar sprites coerentes que realmente melhorem a leitura; evitar pacotes de várias estéticas. Pode redesenhar sprites simples se a ferramenta suportar, ou integrar assets gratuitos com licença documentada. Música de fundo e SFX de coleta, dano, vitória/derrota com fonte e licença; nenhum asset não licenciado.
- UI responsiva a 16:9 e a janela redimensionada. CanvasScaler apropriado, fontes legíveis. Evitar complexidade gráfica excessiva que possa falhar em WebGL.

## Contratos e edge cases relevantes

- `GameController` é estático: inicializar corretamente **toda partida** e ao usar restart, sem contador desatualizado. Conferir comportamento ao dar Play diretamente na cena do jogo; ao menos não começar vencido por contador 0 sem mensagem.
- Contagem de colecionáveis não pode divergir do número efetivo colocado na cena. Evitar decremento duplicado ao receber OnTrigger repetidamente; impedir negativos e dupla finalização.
- Painel de fim deve ser configurado no Inspector em GameObject **ativo**, mesmo que o próprio painel esteja desativado; referências serializadas corretas e sem `Missing Script`.
- Evitar colidir/destruir assets incorretos; tag `Coletavel` precisa existir com case exato.
- Input e movimento em Rigidbody2D devem funcionar sem atravessar paredes e sem diagonais mais rápidas.
- Após vencer ou perder, reiniciar restaura score, vidas, tempo, inimigos e coletáveis. Menu e reinício funcionam repetidamente sem erros.
- Áudio tocando durante transição precisa ser audível e não duplicar de maneira estranha. Sem dependência de caminhos locais inexistentes.
- Os caminhos e o nome real das cenas devem ser inspecionados, não assumidos. Preservar `.meta`/GUIDs e referências de cena, prefab e ScriptableObjects.

## Ordem de execução (gates)

### Gate 0 — Audit e build de segurança

- Inspecione primeiro repo, Unity version, Scene List, scripts, hierarquia, assets, Inspector, referências e Console; indique diferenças do baseline.
- Confirme acesso ao Unity Editor por MCP ou métodos suportados (Editor scripting/batchmode) antes de alterar cenas. Se não houver acesso ou dependência crítica, registre o bloqueio e faça somente o que pode ser realmente verificado.
- Rode o jogo inicial. Se viável, produza **build WebGL de segurança** logo no início e teste que os arquivos básicos de WebGL são gerados. A publicação ainda requer confirmação humana.
- Não remover o projeto atual nem converter templates/pipeline.

### Gate 1 — Mecânica e proteção de penalidades

- Consertar eventuais problemas de estado do tutorial (contagem, início, game over, menu, restart, softlocks).
- Tempo, score, vidas, UI do gameplay, resultado com tempo final e vitória/derrota em ciclo completo.
- Testar duas partidas seguidas, partida vencida, derrota por tempo e derrota por vida.

### Gate 2 — Inimigos e level design

- Redesenhar a arena existente com obstáculos simples, rotas acessíveis e colocação justa de cristais.
- Adicionar patrulheiro e perseguidor, feedback de dano, cooldown de invulnerabilidade.
- Testar paredes, objetivos alcançáveis, colisão e gamepad.

### Gate 3 — Coesão visual e áudio

- Estilizar menu, HUD, arena, jogador, inimigos e cristais; substituir geometria básica onde fizer sentido; sem grafismo desnecessário.
- Adicionar música e efeitos diferentes, com volume razoável e créditos.
- Capturar screenshots de menu, gameplay, dano, vitória e derrota. Corrigir problemas visualmente observados.

### Gate 4 — WebGL/entrega

- Criar/verificar Build Profile Web, incluir menu índice 0 e jogo índice 1.
- Gerar build WebGL final, conferir Console/Player.log e jogabilidade no navegador. Não declarar WebGL funcional só pela presença da pasta Build.
- Preparar ZIP HTML5 correto para itch.io (index.html na raiz do ZIP), README com controles, objetivo, assets/fontes e créditos, imagens de capa e gameplay e texto pronto para página.
- Checklist final por ponto da rubrica, link/localização exata da build, comandos executados, evidências/testes e passos manuais de publicação e Blackboard. Não reivindicar upload ou teste não realizado.

## Expectativas de autonomia e escopo

- Trabalhe sequencialmente; não peça aprovação para decisões triviais de cor/posição/organização. Para decisão arquitetural grande ou mudança incompatível com tutorial, apresente o trade-off primeiro.
- A cada gate: salvar cena/prefabs, compilar, verificar console, executar gameplay, conferir screenshots, fazer commit pequeno se ambiente Git permitir. Não continuar ignorando erros de compilação.
- Caso uma feature enfrente obstáculo alto próximo à entrega, prefira o menor fallback correto e documente o critério parcialmente coberto. Não sacrificar build WebGL, menu, softlock e entrega por polimento visual.
- Não mexer em outros jogos/repositórios, não resetar mudanças do usuário sem autorização, não introduzir pacotes pagos, serviços de terceiros ou dependências difíceis de instalar.
- Não dizer “nota 10 garantida”; o PDF contém uma inconsistência entre pontuação e fórmula, descrita em `RUBRICA_INTRO_UNITY.md`.
- Preservar integridade acadêmica: registrar fontes de código/assets de terceiros, permitir que o aluno compreenda e apresente o que foi feito; pedir confirmação sobre política específica de IA se necessário.

## Entrega esperada do agente

1. Projeto Unity com menus, gameplay e cenas configuradas, pronto para testar.
2. `docs/RUBRICA_INTRO_UNITY.md` marcado com status reais e evidências.
3. Build WebGL final + pasta/ZIP para itch.io, com caminho real; jogo validado em navegador quando possível.
4. README, créditos, texto de itch.io e 1 capa + screenshots; autoria e link GitHub dependem de dados reais.
5. Relatório curto: arquivos alterados, bugs corrigidos, o que foi testado/validado, bloqueios, e instruções manuais exatas restantes.
