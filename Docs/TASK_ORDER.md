# TASK_ORDER

## Ordem Recomendada de Implementação (Após Documentação)

### Fase 0 — Base do Projeto
1. Definir estrutura de pastas (Scripts, Prefabs, Scenes, UI, Data).
2. Criar cena protótipo do Bosque (layout simples).
3. Configurar GameObject de bootstrap (referências globais mínimas).

### Fase 1 — Personagem Jogável
4. Implementar movimentação básica de Rubens (andar, pulo, gravidade).
5. Implementar estado básico de animação com placeholders.
6. Implementar combate simples com katana (hitbox/janela de ataque).

### Fase 2 — Vida, Dano e Loop de Tentativa
7. Implementar sistema de vida e dano (player e inimigos).
8. Implementar morte do jogador e respawn.
9. Implementar checkpoints com ativação e registro do último checkpoint.

### Fase 3 — Inimigos do Bosque
10. Implementar inimigo básico do Bosque (patrulha simples + dano por contato/ataque).
11. Integrar inimigo básico ao sistema de vida/dano.

### Fase 4 — Chefe Lucarelli
12. Implementar arena simples do chefe na mesma cena ou cena dedicada.
13. Implementar comportamento inicial de Lucarelli (MVP de ataque e janela de vulnerabilidade).
14. Integrar derrota de chefe com evento de recompensa.

### Fase 5 — Progressão da Habilidade
15. Implementar sistema de habilidade desbloqueável.
16. Desbloquear e habilitar o **Dash** após Lucarelli.
17. Integrar uso do Dash ao controlador de Rubens.

### Fase 6 — HUD e Polimento Técnico do Protótipo
18. Criar HUD simples para PC (vida e indicador relevante de progresso/habilidade).
19. Revisar fluxo completo do protótipo (spawn -> combate -> chefe -> recompensa).
20. Ajustar bugs críticos e garantir estabilidade mínima da vertical slice.

## Critério de Prioridade
- Sempre concluir um bloco funcional testável antes do próximo.
- Priorizar jogabilidade funcional sobre acabamento visual.
- Manter cada etapa pequena, validável e documentada.
