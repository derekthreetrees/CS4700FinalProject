using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Item equippedWeapon;
    public WeaponHitbox hitbox;
    public SpriteRenderer weaponSprite;
    public float swingAngle = 90f;
    public float swingSpeed = 0.1f;
    private bool canAttack = true;
    public float attackDuration = 0.15f;
    public Vector2 direction = Vector2.right;

    void Update()
    {
        // Capture movement direction (used to aim attacks)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0) direction = new Vector2(x, y).normalized;
    }
    
    public void EquipWeapon(Item weapon)
    {
        equippedWeapon = weapon;
        Debug.Log("Equipped " + weapon.Name);
    }

    public void Attack()
    {
        if (equippedWeapon == null || !canAttack) return;

        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        canAttack = false;

        // position hitbox based on direction
        PositionHitbox();

        // apply weapon damage
        hitbox.damage = equippedWeapon.damage;

        hitbox.gameObject.SetActive(true);
        StartCoroutine(SwingAnimation());
        yield return new WaitForSeconds(attackDuration);
        hitbox.gameObject.SetActive(false);

        // weapon cooldown
        yield return new WaitForSeconds(equippedWeapon.attackCooldown);
        canAttack = true;
    }

    IEnumerator SwingAnimation()
    {
        // weapon sprite should align with attack direction
        float startAngle = -swingAngle / 2f;
        float endAngle = swingAngle / 2f;

        // calculate the base direction
        float directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate swing relative to direction
        weaponSprite.transform.localEulerAngles = new Vector3(0, 0, directionAngle + startAngle);

        // set the position of the sprite in front of the player
        weaponSprite.transform.localPosition = direction * 0.4f;

        weaponSprite.enabled = true;

        float elapsed = 0f;
        while (elapsed < swingSpeed)
        {
            float t = elapsed / swingSpeed;

            // linear rotation through the arc
            float angle = Mathf.Lerp(startAngle, endAngle, t);

            weaponSprite.transform.localEulerAngles = new Vector3(
                0, 0, directionAngle + angle
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        weaponSprite.enabled = false;
    }

    void PositionHitbox()
    {
        // you can tweak the numbers to fit your game
        float distance = 0.5f;
        hitbox.transform.localPosition = direction * distance;
    }
}
