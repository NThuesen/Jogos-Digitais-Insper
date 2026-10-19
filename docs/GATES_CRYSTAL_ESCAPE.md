# Execução por gates — Crystal Escape

## Gate 0 — inspeção e baseline

Lidos integralmente os dois PDFs e os documentos RUBRICA_INTRO_UNITY e CODEX_TASK_INTRO_UNITY antes de alterações de gameplay. Conferida também a fórmula como imagem na página 4 do enunciado: denominador 1625, diferente da soma dos itens. O cabeçalho do PDF diz 2025.2, mas a data expressa de entrega é 20/10/2026 às 23h50. Mantida essa data e nenhuma promessa de nota.

Estado real inicial: Unity 6000.5.8f1, URP 2D, Input System 1.20.0, input Both; MainMenu 0 e SampleScene 1 habilitadas. Perfil Windows usa a lista global. WebGL Support instalado. Player e Triangle são prefabs existentes; há quatro instâncias de Triangle, quatro paredes, áudio de coleta e Canvas de resultado com callbacks válidos. Sem tempo, score, vidas ou inimigos no baseline. Scripts e cenas da estabilização já estavam modificados no working tree; manifest e lock do MCP também. Nada disso foi descartado.

Conexão MCP real confirmada no projeto C:/dev/unity/introUnity. Console inicial sem erros/avisos. O fluxo de tutorial validado na rodada anterior está registrado em VALIDACAO_MCP_2026-10-19.md, sem reimplementação. Backup dos fontes em Builds/IntroUnity-baseline.zip, contendo Assets, Packages e ProjectSettings. Uma tentativa inicial de backup por Compress-Archive encontrou arquivo de física bloqueado pelo Editor; substituída pelo backup Python, sem alterar o arquivo original.

Build WebGL de segurança gerada antes de copiar os scripts do gate 1 para Assets: job build-0b1ad40313, saída Builds/WebGL-Safety. **Aprovada**: 12,44 MB, 1436,19s, zero erros e sete avisos (reconexão do MCP e métodos grandes do TextMesh Pro durante IL2CPP). Abriu no Edge headless, exibiu o menu e não registrou erros no navegador. Captura: evidencias-webgl/safety-menu.png. Backup dos fontes validado por CRC: 156 entradas, nenhum erro.

## Gate 1 — estado da partida

**Aprovado via MCP / Play Mode.** GameController estendido com tempo, score, três vidas e resultado explícito. UIManager inicializa o total real de cristais, mostra HUD e tempo final nas três condições de término. Movimento, coleta e dano deixam de alterar a partida após o fim. Reinício restaura quatro cristais, 0 pontos, 3 vidas e 90s.

Teste de estado executado no Editor: passagem do tempo, vitória somente na quarta coleta, score 400, congelamento após término, timeout em 90s, vidas sem negativos e reset completo. Em Play Mode: quatro triggers reais, vitória e botão de reinício; timeout acelerado para 0,3s usando o mesmo Update; três TakeHit espaçados por 1,35s com rejeição do golpe duplicado imediato; resultado NoLives e painel ativo. O timeout normal de 90s também foi verificado na lógica de estado; o teste visual acelerado não representa uma espera manual de 90s.

Capturas: evidencias-crystal/gate1-timeout.png e gate1-vidas-final.png. Console sem erros. As falhas iniciais dos testes de dano foram intervalos de teste insuficientes; o teste temporizado confirmou a proteção correta. O callback do teste foi ajustado ao tipo EditorApplication.CallbackFunction da versão instalada. Não foi necessário mudar a lógica do jogo por essas falhas do harness.

## Gate 2 — arena, inimigos e controles

**Aprovado via MCP / Play Mode, com teste físico de Xbox pendente.** Mantidos os quatro cristais, jogador e paredes; arena ganhou dois reatores, um patrulheiro e um perseguidor. Contato real do perseguidor reduziu as três vidas e produziu derrota; após derrota, coletas adicionais não alteraram score/estado. Nova partida com ambos ativos venceu com 400 pontos e três vidas.

Teclado D+S e deslocamento para a direita com analógico esquerdo foram simulados pelo Input System real durante 0,6s: deslocamento diagonal (2,12; -2,12), distância 3,00, e cardinal (3,00; 0), distância 3,00; os testes foram feitos antes de ajustar a velocidade de 5 para 4,4. Os dispositivos sintéticos foram removidos. Verificação de caminhos com raio 0,42 contra colliders de paredes e reatores: quatro cristais acessíveis, 2013 células conectadas. Teste de parede com analógico ativo por 1s: x=6,63884, sem atravessar a parede direita, velocidade 4,4.

Correção encontrada: alterações em componentes de uma instância de prefab precisavam ser registradas explicitamente. Gravados os overrides do jogador e das posições dos cristais; recarga da cena confirmou spawn (-5,4; 2,5) e velocidade 4,4. Validação nativa da cena: zero issues/missing scripts/broken prefabs. Console sem erros. O gamepad sintético não é evidência de hardware Xbox físico.

## Gate 3 — arte e áudio

**Aprovado via MCP / Play Mode e inspeção das capturas.** Seis texturas e cinco WAVs originais procedurais, com gerador em Tools/CreateOriginalAssets.py e créditos em CREDITS.md. Mantidos os GUIDs de Player e Triangle. UI com paleta do laboratório, menu com objetivo/controles, HUD, painel com tempo final e seleção do botão para gamepad. Câmera preserva a arena em diferentes proporções.

Música em reprodução no menu e no jogo; jogo com três AudioSources esperados (música, efeitos e coleta), clip Collect no jogador; efeitos distintos de Hit, Victory e Defeat. Vitória e timeout reduziram volume da música de 0,45 para 0,18 e acionaram os respectivos resultados. Conferência auditiva humana de volume permanece manual.

Capturas: menu-final.png, gameplay-final-corrigido.png, dano-final.png, vitoria-final.png e derrota-final.png em evidencias-crystal. Corrigido contraste do rodapé após inspeção visual. Console sem erros/avisos ao fim dos testes; cenas sem scripts ou prefabs quebrados.

## Gate 4 — WebGL e entrega

**Aprovado para entrega local, com avisos e limites registrados.** Perfil Assets/Settings/Build Profiles/WebGL.asset criado pela API real do Editor, build target20/WebGL, Scene List global. Template responsivo e arquivos sem compressão dependente de headers em Assets/WebGLTemplates/CrystalEscape. Revisão final: job build-b4ec5bc6f9, relatório nativo Succeeded/WebGL, 59.863.577 bytes,605,27s,zero erros e quatro avisos (três de métodos grandes do TMP e um sobre compilação de importadores do Editor). A troca para FindAnyObjectByType foi comprovada no C++ gerado da build; nenhum importador/pós-processador foi alterado.

Edge headless/SwiftShader: duas vitórias, restart, menu, novo jogo, redimensionamento4:3, derrota após90s reais e restart após derrota. Zero erros HTTP/runtime. A macro encontrou interferência do perseguidor na segunda rota e precisou continuar; só as imagens efetivamente inspecionadas são evidências de aprovação. O aviso FSR do URP permanece, sem falha visual observada; detalhes completos em [VALIDACAO_FINAL.md](VALIDACAO_FINAL.md).

ZIP validado por CRC com index.html na raiz: Builds/Release/CrystalEscape-WebGL.zip,19.061.960 bytes. Capa, menu, gameplay, dano, vitória, derrota, créditos e texto da página preparados em Builds/Release. Autor informado: Nicholas Thuesen. Xbox físico não testado por indisponibilidade de controle. Conferência auditiva humana e publicações externas continuam pendentes.

## Evidência e limites gerais

Transcrição de chamadas, resultados e testes: RUBRICA_MCP.jsonl. Operações temporárias por execute_code não criam sistemas de teste no jogo publicado. O leitor HTTP foi ajustado para lidar com notificações SSE e para enviar texto C# sem metadados do PowerShell; chamadas antigas travadas foram encerradas antes de repetir a configuração. Problemas do harness estão separados dos resultados do jogo.

GitHub público confirmado em GITHUB_STATUS.json. Push dos novos arquivos, publicação no itch.io/jam e envio no Blackboard são etapas externas ainda não concluídas. Nome completo preenchido no material local. As alterações preexistentes foram preservadas.
