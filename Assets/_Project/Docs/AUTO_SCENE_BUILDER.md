# AUTO_SCENE_BUILDER

## Objetivo
Gerar uma cena jogavel de teste do Bosque da Nevoa Perdida com placeholders simples e os sistemas atuais do prototipo.

A ferramenta monta Rubens, camera, piso, plataformas, inimigos, checkpoint, Dash gate, Lucarelli, HUD e menu de pausa sem usar arte externa.

## Usar o menu
1. Abra o projeto no Unity Editor.
2. Espere a compilacao terminar.
3. Use `Tools > Tester > Build Prototype Scene`.
4. Se houver cena alterada aberta, escolha se quer salvar antes de o builder criar a cena de teste.
5. A ferramenta cria ou reconstrui:
   - `Assets/_Project/Scenes/Prototype_Bosque_Test.unity`.

O builder tambem tenta:
- garantir as Tags `Player` e `Interactable`;
- garantir as Layers `Player`, `Ground`, `Enemy` e `Interactable`;
- incluir a cena gerada em Build Settings.

Se nao houver slot de Layer livre, o Console registra o aviso e a Layer correspondente precisa ser ajustada manualmente.

## Abrir a cena criada
Depois de rodar o menu:
1. A cena `Prototype_Bosque_Test` ja fica aberta.
2. Para reabrir depois, use o Project window em `Assets/_Project/Scenes`.
3. Salve ajustes locais de layout apenas se quiser manter uma variante do teste.

Rodar o menu novamente reconstrui a cena automatica com o layout inicial do builder.

## Conteudo montado
### Rubens
- `Rubens_Player` com sprite placeholder, `Rigidbody2D`, `BoxCollider2D`, `PlayerController2D`, `PlayerHealth`, `PlayerCombat` e `AbilityManager`.
- Filho `GroundCheck` ligado ao controller.
- Filho `AttackPoint` ligado ao combate.
- Dash inicia bloqueado.

### Sala
- Piso principal e plataformas na Layer `Ground`.
- Dois inimigos basicos na Layer `Enemy`.
- Checkpoint trigger com `RespawnPoint`.
- Barreira com `AbilityGate` depois da area de Lucarelli.
- Lucarelli em uma arena simples sobre o mesmo piso de teste.

### Camera e UI
- `Main Camera` ortografica com `CameraFollow2D` apontando para Rubens.
- `GameManager` na cena.
- Canvas com HUD de vida, estado de Dash e mensagens.
- `PausePanel` simples com botoes `Continuar`, `Reiniciar` e `Sair`.
- `EventSystem` para clique dos botoes.

## Apertar Play e testar o fluxo
1. Rode o builder e pressione Play.
2. Ande com `A`/`D` ou setas.
3. Pule com `Space`.
4. Ataque os inimigos com `J`.
5. Encoste em um inimigo e confira dano e HUD.
6. Atravesse o checkpoint e confirme a mensagem de ativacao.
7. Para testar morte rapido, use `PlayerHealth > Debug/Take 10 Damage` varias vezes ou deixe os inimigos causarem dano.
8. Confirme que Rubens respawna no ultimo checkpoint com vida restaurada.
9. Tente cruzar `DashGate_01` antes do Dash para ver o bloqueio.
10. Derrote Lucarelli com a katana para liberar o Dash.
11. Use `Left Shift` para Dash e atravesse a barreira.
12. Pressione `ESC` para abrir e fechar o menu de pausa.
13. Nos botoes da pausa, teste `Continuar`, `Reiniciar` e o log de `Sair` no Editor.

Para testar a barreira antes de Lucarelli sem lutar, use o menu de contexto `AbilityManager > Debug/Unlock Dash` somente como atalho de prototipo.

## Campos ligados automaticamente
O builder liga no Inspector:
- `GroundCheck`, `Ground Layer` e `AbilityManager` do `PlayerController2D`;
- `AttackPoint` e `Enemy Layers` do `PlayerCombat`;
- target da `CameraFollow2D`;
- textos, `PlayerHealth` e `AbilityManager` do `HUDController`;
- painel do `PauseMenuController`;
- botoes do painel de pausa;
- `RespawnPoint` do checkpoint;
- collider, visual e `AbilityManager` do Dash gate;
- alvo e recompensa de Dash de Lucarelli.

## Ajustes manuais possiveis
A cena deve funcionar ao apertar Play depois do builder. Ainda pode valer ajustar manualmente:
- raio e posicao de `GroundCheck` se o placeholder de Rubens mudar;
- posicao de `AttackPoint` e raio da katana para outro tamanho de sprite;
- velocidade, vida, dano e patrulha dos inimigos;
- vida, intervalos e raios dos ataques de Lucarelli;
- enquadramento e suavizacao da camera;
- layout visual e tamanho dos textos do HUD/pausa;
- cena em Build Settings se o projeto tiver uma regra propria de ordem de cenas.

## Limitacoes da cena automatica
- O layout e uma sala mecanica de teste, nao um mapa final do Bosque.
- Placeholders usam recursos simples da propria Unity.
- Lucarelli continua com a IA inicial sem telegraph visual final e sem segunda fase.
- Inimigos patrulham por distancia e nao detectam beirada.
- Checkpoint e Dash nao usam save persistente.
- HUD e pausa usam UI simples de teste.
- Rodar o builder novamente substitui a cena automatica salva no caminho padrao.
