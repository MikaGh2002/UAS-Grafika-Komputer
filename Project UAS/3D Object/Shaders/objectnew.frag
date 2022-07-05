#version 330 core
out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 diffPow;
uniform float ambiPow;
uniform float specPow;

uniform vec3 bulanPos;
uniform vec3 senterPos;
uniform vec3 viewPos; 
uniform vec3 spotDir;
uniform float spotAngle;

in vec3 Normal; 
in vec3 FragPos; 
vec3 cahayaBulan = vec3(1.0f,1.0f,1.0f);

vec3 CalcSpot(vec3 norm) {
    //Ambient
    vec3 ambient;

    // Normalize
    vec3 lightDir = normalize(senterPos - FragPos);

    vec3 diffuse;
    vec3 specular;
    if (dot(lightDir, spotDir) >= spotAngle)
    {
        //Ambient 
        ambient = lightColor * ambiPow;

        //Diffuse
        float diff = max(dot(norm, lightDir), 0);
        diffuse = diff * lightColor;


        //Specular
        vec3 viewDir = normalize(viewPos - FragPos);
        vec3 reflectDir = reflect(-lightDir, norm);
        float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
        //vec3 specular = specularStrength * spec * lightColor;
        specular = specPow * spec * lightColor;
    }
    else
    {
        vec3 diffuse = vec3(0,0,0);
        vec3 specular = vec3(0,0,0);
        vec3 ambient = vec3(0,0,0);
    }

    vec3 result = (ambient + diffuse + specular) * vec3(objectColor);
    return result;

}


void main()
{

    vec3 ambient = 0.001f * cahayaBulan;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(bulanPos - FragPos); 
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * cahayaBulan * diffPow;
    vec3 result = ((ambient + diffuse) * objectColor);
    result += CalcSpot(norm);
    FragColor = vec4(result, 1.0);
}