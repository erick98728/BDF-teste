# BOSQUE_ROOM_SETUP (Sala Protótipo Jogável - Bosque da Névoa Perdida)

Este guia monta uma sala pequena e objetiva para validar:
- movimentação (andar/pular)
- combate (katana)
- dano e morte
- checkpoint/respawn
- bloqueio por Dash

## 1) Criar e salvar a cena
1. Crie uma nova cena 2D.
2. Salve em `Assets/_Project/Scenes/Bosque_Prototype_Room`.

## 2) Hierarquia sugerida
Crie os seguintes objetos raiz:
- `Level_Geometry`
- `Gameplay`
- `Player`
- `Enemies`
- `Checkpoints`
- `Gates`
- `BossExit`

## 3) Montar chão e plataformas (placeholders)
Dentro de `Level_Geometry`, crie sprites `Square` com `BoxCollider2D`:

1. `Ground_Main`
   - Position: `(0, -2, 0)`
   - Scale: `(24, 1, 1)`
   - Layer: `Ground`
2. `Platform_Left`
   - Position: `(-4, 0, 0)`
   - Scale: `(4, 0.7, 1)`
   - Layer: `Ground`
3. `Platform_Mid`
   - Position: `(3, 1.2, 0)`
   - Scale: `(4, 0.7, 1)`
   - Layer: `Ground`
4. `Platform_Right`
   - Position: `(9, 2.2, 0)`
   - Scale: `(3.5, 0.7, 1)`
   - Layer: `Ground`

> Use cores diferentes para facilitar leitura visual (placeholder).

## 4) Criar Rubens (Player)
1. Em `Player`, crie `Rubens` (Square).
2. Position inicial: `(-9, -1, 0)`.
3. Adicione componentes:
   - `Rigidbody2D` (`Gravity Scale` ~4, `Freeze Rotation Z` marcado)
   - `BoxCollider2D`
   - `PlayerController2D`
   - `PlayerHealth`
   - `AbilityManager`
   - `PlayerCombat`
4. Crie filho `GroundCheck` e posicione em `(0, -0.55, 0)`.
5. Crie filho `AttackPoint` e posicione em `(0.6, 0, 0)`.
6. Ligue no Inspector:
   - `PlayerController2D > Ground Check = GroundCheck`
   - `PlayerController2D > Ground Layer = Ground`
   - `PlayerCombat > Attack Point = AttackPoint`
   - `PlayerCombat > Enemy Layer = Enemy`

## 5) Configurar câmera
1. Na `Main Camera`, adicione `CameraFollow2D`.
2. `Target = Rubens`.
3. Offset sugerido: `(0, 1, -10)`.
4. Smooth Time: `0.1` a `0.2`.

## 6) Criar inimigos básicos (2 ou 3)
Crie em `Enemies` 3 objetos `Square` com Layer `Enemy`:

### Enemy_01
- Position: `(-2, -1, 0)`
- Componentes:
  - `Rigidbody2D` (`Gravity Scale` 0, `Freeze Rotation Z`)
  - `BoxCollider2D`
  - `EnemyBase`
  - `BasicPatrolEnemy`
- Valores sugeridos:
  - `Max Health`: 30
  - `Move Speed`: 1.8
  - `Patrol Distance`: 2.5
  - `Contact Damage`: 10

### Enemy_02
- Position: `(5, 2, 0)`
- Mesmo setup, com `Patrol Distance` 1.8.

### Enemy_03 (opcional)
- Position: `(10, 2.8, 0)`
- Mesmo setup, com `Move Speed` 2.2.

## 7) Criar checkpoint
1. Em `Checkpoints`, crie `Checkpoint_01`.
2. Position sugerida: `(1, -1, 0)`.
3. Adicione `BoxCollider2D` com `Is Trigger = true`.
4. Adicione script `Checkpoint`.
5. (Opcional) adicione sprite simples para visualização do totem.

## 8) Criar bloqueio por Dash
1. Em `Gates`, crie `DashGate_01`.
2. Position sugerida: `(12, 3.2, 0)` (corredor para saída).
3. Scale sugerida: `(1.2, 3, 1)`.
4. Adicione `BoxCollider2D` (não-trigger para bloquear fisicamente).
5. Adicione script `AbilityGate`:
   - `Required Ability = Dash`
   - `Disable Visual When Unlocked = true`

## 9) Criar área final (pré-Luccarelli)
1. Em `BossExit`, crie `Lucarelli_Entrance_Placeholder`.
2. Position sugerida: `(15, 3.2, 0)`.
3. Use sprite/placa placeholder com texto: `Arena do Lucarelli (futuro)`.
4. Opcional: `BoxCollider2D Is Trigger` para detectar chegada do jogador.

## 10) GameManager e HUD (mínimo)
1. Crie objeto `GameManager` na cena e adicione script `GameManager`.
2. HUD visual complexa não é necessária; `HUDController` pode ficar opcional neste passo.

## 11) Fluxo de teste recomendado
1. Iniciar na esquerda com Rubens.
2. Andar e pular plataformas.
3. Lutar com inimigos (katana).
4. Passar no `Checkpoint_01` (ver log de ativação).
5. Tomar dano/morrer e validar respawn no checkpoint.
6. Ir até `DashGate_01` sem dash: deve bloquear.
7. Desbloquear dash (debug `F6` no `AbilityManager` se habilitado, ou `UnlockDash()` por evento).
8. Encostar no gate novamente: deve liberar passagem.
9. Acessar `Lucarelli_Entrance_Placeholder`.

## 12) Elementos placeholder nesta sala
- Sprites quadrados para chão, Rubens, inimigos, checkpoint e gate.
- Sem animações finais.
- Sem VFX/SFX finais.
- Entrada de chefe apenas como marcador de layout.
