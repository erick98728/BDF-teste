# TUTORIAL_TEXTS

## Objetivo
Documentar as mensagens curtas da `Prototype_Bosque_Demo` para orientar controles, caminhos, quedas, checkpoints, Dash, Lucarelli e fim da demo sem criar cutscenes ou dialogos longos.

As mensagens sao exibidas por objetos com `TutorialSign`, `Collider2D` marcado como trigger e visual placeholder simples no mundo.

## Textos da demo
| Area | GameObject | Mensagem | Objetivo |
| --- | --- | --- | --- |
| Entrada do Bosque | `Sign_Entrance_Movement` | `Use A/D ou as setas para se mover. Espaço para pular.` | Ensinar movimento e pulo logo no spawn seguro. |
| Antes do primeiro inimigo | `Sign_FirstEnemy_Katana` | `Pressione J para atacar com a katana.` | Ensinar ataque antes do primeiro combate real. |
| Depois do combate inicial | `Sign_Checkpoint_ReturnPoint` | `Checkpoints definem seu ponto de retorno após uma queda ou derrota.` | Explicar respawn no primeiro checkpoint seguro, sem tutorial longo. |
| Antes do primeiro buraco | `Sign_FirstPit_Caution` | `Cuidado com os buracos. Se cair, você volta ao último checkpoint.` | Preparar o jogador para `DeathZone` e respawn. |
| Hub Central | `Sign_CentralHub_Paths` | `O Bosque se divide em vários caminhos. Observe os marcos e siga com atenção.` | Explicar que o mapa deixou de ser linha reta. |
| Caminho superior | `Sign_UpperCanopy_Parkour` | `As copas exigem precisão nos pulos. Suba com calma.` | Sinalizar parkour vertical sem Dash. |
| Caminho inferior | `Sign_LowerRoots_Warning` | `As raízes escondem quedas e passagens. Avance com cuidado.` | Sinalizar rota baixa com buracos e retornos. |
| DashGate | `Sign_DashGate_Blocked` | `Uma força bloqueia o caminho. Talvez uma nova técnica permita atravessar.` | Fazer o jogador memorizar o gate antes de Lucarelli. |
| Rota de Lucarelli | `Sign_LucarelliPath_Prepare` | `Lucarelli guarda a passagem. Prepare-se antes de avançar.` | Avisar que a luta de chefe esta proxima. |
| Derrota de Lucarelli | `HUDController` via `AbilityManager` | `Dash desbloqueado. Use Shift esquerdo para avançar rapidamente.` | Confirmar a recompensa do chefe e ensinar a tecla do Dash. |
| Area pos-Dash | `Sign_PostDash_Parkour` | `Use Shift esquerdo para cruzar vãos maiores.` | Reforcar uso do Dash no desafio final. |
| Fim da demo | `Sign_DemoEnd_Thanks` | `Fim da demo. Obrigado por jogar esta versão de teste.` | Encerrar claramente a build de teste. |

## Como testar os triggers
1. Gere a cena em `Tools > Tester > Build Bosque Demo Scene`.
2. Abra `Assets/_Project/Scenes/Prototype_Bosque_Demo.unity`.
3. Entre em Play Mode.
4. Passe Rubens pela area de cada placa.
5. Confirme que o texto aparece no HUD e some depois de alguns segundos.
6. Atravesse a mesma placa novamente e confirme que ela nao repete se `Show Once` estiver ligado.
7. Desative temporariamente o Canvas ou o `HUDController`, se quiser validar o fallback por `Debug.Log`.

## Como alterar textos no Inspector
1. Na Hierarchy, abra `Tutorial > Signs`.
2. Selecione o objeto `Sign_*` desejado.
3. No componente `TutorialSign`, altere:
   - `Message`;
   - `Show Once`;
   - `Message Duration`.
4. Ajuste o `BoxCollider2D` trigger se a mensagem aparecer cedo ou tarde demais.
5. O visual da placa fica nos filhos `VisualPlate`, `Lantern` e `Label`.

## Placeholders atuais
- Placas, lanternas e labels sao formas simples geradas pelo builder.
- Nao ha dialogo, retrato, voz, animacao, cutscene ou sistema narrativo.
- Os textos sao de teste e podem mudar depois de playtests com amigos.
- O `TutorialSign` apenas mostra mensagens; ele nao salva progresso e nao altera habilidades.
