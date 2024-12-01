import json

# 定义关键帧类
class Keyframe:
    def __init__(self, time, attributes):
        """
        初始化关键帧。
        :param time: 关键帧时间。
        :param attributes: 与关键帧相关的属性（如旋转角度、位置等）。
        """
        self.time = time
        self.attributes = attributes

    def __repr__(self):
        return f"<Keyframe time={self.time}, attributes={self.attributes}>"

# 定义动画类
class Animation:
    def __init__(self, name):
        """
        初始化动画。
        :param name: 动画名称。
        """
        self.name = name
        self.bones = {}
        self.slots = {}

    def add_bone_animation(self, bone_name, property_name, keyframes):
        """
        添加骨骼动画。
        :param bone_name: 骨骼名称。
        :param property_name: 动画属性名称（如 rotate, translate）。
        :param keyframes: 关键帧列表。
        """
        if bone_name not in self.bones:
            self.bones[bone_name] = {}
        self.bones[bone_name][property_name] = keyframes

    def add_slot_animation(self, slot_name, property_name, keyframes):
        """
        添加插槽动画。
        :param slot_name: 插槽名称。
        :param property_name: 动画属性名称（如 attachment, color）。
        :param keyframes: 关键帧列表。
        """
        if slot_name not in self.slots:
            self.slots[slot_name] = {}
        self.slots[slot_name][property_name] = keyframes

    def __repr__(self):
        return f"<Animation name={self.name}, bones={self.bones}, slots={self.slots}>"

# 解析动画 JSON 文件
def parse_spine_json(file_path):
    with open(file_path, "r",encoding="utf-8") as file:
        data = json.load(file)

    animations_data = data.get("animations", {})
    animations = []

    for anim_name, anim_content in animations_data.items():
        animation = Animation(anim_name)

        # 解析骨骼动画
        bones = anim_content.get("bones", {})
        for bone_name, bone_properties in bones.items():
            for prop_name, keyframes in bone_properties.items():
                parsed_keyframes = [
                    Keyframe(keyframe.get("time", 0), {k: v for k, v in keyframe.items() if k != "time"})
                    for keyframe in keyframes
                ]
                animation.add_bone_animation(bone_name, prop_name, parsed_keyframes)

        # 解析插槽动画
        slots = anim_content.get("slots", {})
        for slot_name, slot_properties in slots.items():
            for prop_name, keyframes in slot_properties.items():
                parsed_keyframes = [
                    Keyframe(keyframe.get("time", 0), {k: v for k, v in keyframe.items() if k != "time"})
                    for keyframe in keyframes
                ]
                animation.add_slot_animation(slot_name, prop_name, parsed_keyframes)

        animations.append(animation)

    return animations

# 将时间线转换为 Cocos2d 动作格式
def generate_cocos_action(animation):
    action_scripts = []

    for bone_name, properties in animation.bones.items():
        for prop_name, keyframes in properties.items():
            actions = []

            for i in range(len(keyframes) - 1):
                current_frame = keyframes[i]
                next_frame = keyframes[i + 1]

                duration = next_frame.time - current_frame.time
                value = next_frame.attributes.get("value", 0)

                if prop_name == "rotate":
                    actions.append(f"CCRotateTo({duration:.3f}, {value})")
                elif prop_name == "translate":
                    x = next_frame.attributes.get("x", 0)
                    y = next_frame.attributes.get("y", 0)
                    actions.append(f"CCMoveTo({duration:.3f}, ccp({x}, {y}))")
                elif prop_name == "scale":
                    scale_x = next_frame.attributes.get("x", 1)
                    scale_y = next_frame.attributes.get("y", 1)
                    actions.append(f"CCScaleTo({duration:.3f}, {scale_x}, {scale_y})")

            if actions:
                action_scripts.append(f"// Bone: {bone_name}, Property: {prop_name}")
                action_scripts.append(f"CCSequence::create({', '.join(actions)}, NULL)")

    return "\n".join(action_scripts)

# 示例用法
if __name__ == "__main__":
    file_path = "data.json"  # 替换为你的 JSON 文件路径
    animations = parse_spine_json(file_path)

    for animation in animations:
        print(f"// Animation: {animation.name}")
        cocos_script = generate_cocos_action(animation)
        print(cocos_script)
